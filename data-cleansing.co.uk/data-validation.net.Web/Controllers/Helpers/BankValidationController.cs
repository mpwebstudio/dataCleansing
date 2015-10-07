namespace data_validation.net.Web.Controllers.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;

    using data_cleansing.net.Data;
    using data_validation.net.Web.ViewModels;
    using System.Text.RegularExpressions;
    using System.Numerics;
    public class BankValidationController
    {
        public class IbanData
        {
            public string CountryCode;
            public int Lenght;
            public string RegexStructure;
            public bool IsEU924;
            public string Sample;

            public IbanData()
            {

            }
            public IbanData(string countryCode, int lenght, string regexStructure, bool isEU924, string sample)
                : this()
            {
                CountryCode = countryCode;
                Lenght = lenght;
                RegexStructure = regexStructure;
                IsEU924 = isEU924;
                Sample = sample;
            }
        }
        public class StatusData
        {
            public bool IsValid;
            public string Message;

            public StatusData(bool isValid, string message)
            {
                IsValid = isValid;
                Message = message;
            }
        }


        public static class Iban
        {
            // error messages
            // you can translate this to your language
            static string[] errorMessages =
        {
            "The IBAN contains illegal characters.",
			"The structure of IBAN is wrong.",
			"The check digits of IBAN are wrong.",
			"IBAN for country {0} currently is not avaliable.",
			"The IBAN of {0} needs to be {1} characters long.",
			"The country specific structure of IBAN is wrong.",
			"The IBAN is incorrect.",
			"The IBAN seems to be correct.",
            
        };

            /// <summary>
            /// Main method that checks if the supplied IBAN code is valid
            /// </summary>
            /// <typeparam name="iban">IBAN code to be checked</typeparam>
            /// <typeparam name="cleanText">If your IBAN code contains white space and/or not all capital you can pass true to this method. 
            /// If true it will clear and capitalize the supplied IBAN number</typeparam>
            public static StatusData CheckIban(string iban, bool cleanText)
            {
                try
                {
                    if (cleanText)
                        iban = iban.Replace(" ", "").ToUpper(); // remove empty space & convert all uppercase

                    if (Regex.IsMatch(iban, @"\W")) // contains chars other than (a-zA-Z0-9)
                        return new StatusData(false, errorMessages[0]);

                    if (!Regex.IsMatch(iban, @"^\D\D\d\d.+")) // first chars are letter letter digit digit
                        return new StatusData(false, errorMessages[1]);

                    if (Regex.IsMatch(iban, @"^\D\D00.+|^\D\D01.+|^\D\D99.+")) // check digit are 00 or 01 or 99
                        return new StatusData(false, errorMessages[2]);

                    string countryCode = iban.Substring(0, 2);

                    IbanData currentIbanData = (from id in IBANList()
                                                where id.CountryCode == countryCode
                                                select id).FirstOrDefault();

                    if (currentIbanData == null) // test if country respected
                        return new StatusData(false, string.Format(errorMessages[3], countryCode));

                    if (iban.Length != currentIbanData.Lenght) // fits length to country
                        return new StatusData(false, string.Format(errorMessages[4], countryCode, currentIbanData.Lenght));

                    if (!Regex.IsMatch(iban.Remove(0, 4), currentIbanData.RegexStructure)) // check country specific structure
                        return new StatusData(false, errorMessages[5]);

                    // ******* from wikipedia.org
                    //The checksum is a basic ISO 7064 mod 97-10 calculation where the remainder must equal 1.
                    //To validate the checksum:
                    //1- Check that the total IBAN length is correct as per the country. If not, the IBAN is invalid. 
                    //2- Move the four initial characters to the end of the string. 
                    //3- Replace each letter in the string with two digits, thereby expanding the string, where A=10, B=11, ..., Z=35. 
                    //4- Interpret the string as a decimal integer and compute the remainder of that number on division by 97. 
                    //The IBAN number can only be valid if the remainder is 1.
                    string modifiedIban = iban.ToUpper().Substring(4) + iban.Substring(0, 4);
                    modifiedIban = Regex.Replace(modifiedIban, @"\D", m => ((int)m.Value[0] - 55).ToString());

                    int remainer = 0;
                    while (modifiedIban.Length >= 7)
                    {
                        remainer = int.Parse(remainer + modifiedIban.Substring(0, 7)) % 97;
                        modifiedIban = modifiedIban.Substring(7);
                    }
                    remainer = int.Parse(remainer + modifiedIban) % 97;

                    if (remainer != 1)
                    {
                        return new StatusData(false, errorMessages[6]);
                    }

                    //2 check digits Mod 97 - 10 
                    string checkDigits = iban.ToUpper().Substring(4) + iban.Substring(0, 2) + "00";
                    checkDigits = Regex.Replace(checkDigits, @"\D", m => ((int)m.Value[0] - 55).ToString());

                    BigInteger checkDigit = ((BigInteger.Parse(checkDigits) % 97) + 97) % 97;

                    if (int.Parse(iban.Substring(2, 2)) != (98 - checkDigit))
                    {
                        return new StatusData(false, errorMessages[2]);
                    }

                    // Check digit for the 7 characters of the BIC
                    if (countryCode == "AL")
                    {
                        var kibArray = iban.Substring(4, 7).ToCharArray();

                        var kibS = (int)Char.GetNumericValue(kibArray[0]) * 9 + (int)Char.GetNumericValue(kibArray[1]) * 7 + (int)Char.GetNumericValue(kibArray[2]) * 3 + (int)Char.GetNumericValue(kibArray[3]) * 1 + (int)Char.GetNumericValue(kibArray[4]) * 9 + (int)Char.GetNumericValue(kibArray[5]) * 7 + (int)Char.GetNumericValue(kibArray[6]) * 3;

                        var kib = 10 - ((kibS % 10) + 10) % 10;

                        if (kib.ToString() != iban.Substring(11, 1))
                        {
                            return new StatusData(false, errorMessages[1]);
                        }

                    }

                    //Check last 2 control digits in the Belgium IBAN
                    if (countryCode == "BE")
                    {
                        var lastTwo = iban.Substring(14, 2);

                        var dummyNumb = iban.Substring(4, 10);

                        var dummyNumbAsDigit = long.Parse(dummyNumb);

                        var dummyResult = ((dummyNumbAsDigit % 97) + 97) % 97;

                        if (dummyResult != int.Parse(lastTwo))
                        {
                            return new StatusData(false, errorMessages[1]);
                        }
                    }

                    if (countryCode == "FR" || countryCode == "MC")
                    {
                        var lastTwo = int.Parse(iban.Substring(25, 2));

                        var bankCode = long.Parse(iban.Substring(4, 5));

                        var branchCode = long.Parse(iban.Substring(9, 5));

                        var accountNumber = iban.Substring(14, 11);

                        accountNumber = Regex.Replace(accountNumber, @"\D", ConversionController.FranceToDigit, RegexOptions.IgnorePatternWhitespace);

                        var rib = 97 - (((89 * bankCode + 15 * branchCode + 3 * long.Parse(accountNumber)) % 97) + 97) % 97;

                        if (lastTwo != rib)
                        {
                            return new StatusData(false, errorMessages[1]);
                        }
                    }

                    if (countryCode == "IT")
                    {
                        var abi = iban.Substring(5, 5).ToCharArray();

                        var cab = iban.Substring(10, 5).ToCharArray();

                        var accountNumber = iban.Substring(15, 12).ToCharArray();

                        var abiConvert = ConversionController.OddNumber(abi[0]) + ConversionController.EvenNumber(abi[1]) + ConversionController.OddNumber(abi[2]) + ConversionController.EvenNumber(abi[3]) + ConversionController.OddNumber(abi[4]);

                        var cabConvert = ConversionController.EvenNumber(cab[0]) + ConversionController.OddNumber(cab[1]) + ConversionController.EvenNumber(cab[2]) + ConversionController.OddNumber(cab[3]) + ConversionController.EvenNumber(cab[4]);

                        var accNumbConvert = ConversionController.OddNumber(accountNumber[0]) + ConversionController.EvenNumber(accountNumber[1]) + ConversionController.OddNumber(accountNumber[2]) + ConversionController.EvenNumber(accountNumber[3]) + ConversionController.OddNumber(accountNumber[4]) + ConversionController.EvenNumber(accountNumber[5]) + ConversionController.OddNumber(accountNumber[6]) + ConversionController.EvenNumber(accountNumber[7]) + ConversionController.OddNumber(accountNumber[8]) + ConversionController.EvenNumber(accountNumber[9]) + ConversionController.OddNumber(accountNumber[10]) + ConversionController.EvenNumber(accountNumber[11]);

                        var cin = (((abiConvert + cabConvert + accNumbConvert) % 26) + 26) % 26;

                        if (ConversionController.CinToChar(cin) != iban.Substring(4, 1))
                        {
                            return new StatusData(false, errorMessages[1]);
                        }

                    }

                    if (countryCode == "PT")
                    {
                        var nib = iban.Substring(4, 21);

                        var checkedDigit = BigInteger.Parse(nib);

                        checkedDigit = ((checkedDigit % 97) + 97) % 97;

                        if (checkedDigit != 1)
                        {
                            return new StatusData(false, errorMessages[1]);
                        }

                    }

                    if (countryCode == "ES")
                    {
                        var firstDigits = iban.Substring(4, 8);

                        var firstDigitSum = 11 - (((int)Char.GetNumericValue(firstDigits[0]) * 4 + (int)Char.GetNumericValue(firstDigits[1]) * 8 + (int)Char.GetNumericValue(firstDigits[2]) * 5 + (int)Char.GetNumericValue(firstDigits[3]) * 10 + (int)Char.GetNumericValue(firstDigits[4]) * 9 + (int)Char.GetNumericValue(firstDigits[5]) * 7 + (int)Char.GetNumericValue(firstDigits[6]) * 3 + (int)Char.GetNumericValue(firstDigits[7]) * 6) % 11);

                        firstDigitSum = firstDigitSum == 11 ? 0 : firstDigitSum == 10 ? 1 : firstDigitSum;

                        var secondDigits = iban.Substring(14, 10).ToArray();

                        var secondDigitSum = 11 - (((int)Char.GetNumericValue(secondDigits[0]) * 1 + (int)Char.GetNumericValue(secondDigits[1]) * 2 + (int)Char.GetNumericValue(secondDigits[2]) * 4 + (int)Char.GetNumericValue(secondDigits[3]) * 8 + (int)Char.GetNumericValue(secondDigits[4]) * 5 + (int)Char.GetNumericValue(secondDigits[5]) * 10 + (int)Char.GetNumericValue(secondDigits[6]) * 9 + (int)Char.GetNumericValue(secondDigits[7]) * 7 + (int)Char.GetNumericValue(secondDigits[8]) * 3 + (int)Char.GetNumericValue(secondDigits[9]) * 6) % 11);

                        secondDigitSum = secondDigitSum == 11 ? 0 : secondDigitSum == 10 ? 1 : secondDigitSum;

                        if (iban.Substring(12, 2) != firstDigitSum.ToString() + secondDigitSum.ToString())
                        {
                            return new StatusData(false, errorMessages[1]);
                        }
                    }

                    return new StatusData(true, errorMessages[7]);
                }
                catch (Exception)
                {
                    return new StatusData(false, "(exception) " + errorMessages[6]);
                }
            }

            /// <summary>
            /// If you develop another code/algorithm you can use this list
            /// check updates @ http://www.tbg5-finance.org/checkiban.js
            /// </summary>
            public static List<IbanData> IBANList()
            {
                List<IbanData> newList = new List<IbanData>();
                newList.Add(new IbanData("AD", 24, @"\d{8}[a-zA-Z0-9]{12}", false, "AD1200012030200359100100"));
                newList.Add(new IbanData("AL", 28, @"\d{8}[a-zA-Z0-9]{16}", false, "AL47212110090000000235698741"));
                newList.Add(new IbanData("AT", 20, @"\d{16}", true, "AT611904300234573201"));
                newList.Add(new IbanData("AZ", 28, @"\[a-zA-Z0-9]{4}\d{20}", false, "AZ96IBAZ38090019449748533210"));
                newList.Add(new IbanData("AE", 23, @"\d{19}", false, "AE611904300234573201"));
                newList.Add(new IbanData("BA", 20, @"\d{16}", false, "BA391290079401028494"));
                newList.Add(new IbanData("BE", 16, @"\d{12}", true, "BE68539007547034"));
                newList.Add(new IbanData("BG", 22, @"[A-Z]{4}\d{6}[a-zA-Z0-9]{8}", true, "BG80BNBG96611020345678"));
                newList.Add(new IbanData("BH", 22, @"\[A-Z]{4}[a-zA-Z0-9]{14}", false, "BH29BMAG1299123456BH00"));
                newList.Add(new IbanData("BR", 29, @"\d{23}[a-zA-Z0-9]{2}", false, "BR9700360305000010009795493P1"));
                newList.Add(new IbanData("CH", 21, @"\d{5}[a-zA-Z0-9]{12}", false, "CH9300762011623852957"));
                newList.Add(new IbanData("CY", 28, @"\d{8}[a-zA-Z0-9]{16}", true, "CY17002001280000001200527600"));
                newList.Add(new IbanData("CZ", 24, @"\d{20}", true, "CZ6508000000192000145399"));
                newList.Add(new IbanData("DE", 22, @"\d{18}", true, "DE89370400440532013000"));
                newList.Add(new IbanData("DK", 18, @"\d{14}", true, "DK5000400440116243"));
                newList.Add(new IbanData("EE", 20, @"\d{16}", true, "EE382200221020145685"));
                newList.Add(new IbanData("ES", 24, @"\d{20}", true, "ES9121000418450200051332"));
                newList.Add(new IbanData("FI", 18, @"\d{14}", true, "FI2112345600000785"));
                newList.Add(new IbanData("FO", 18, @"\d{14}", false, "FO6264600001631634"));
                newList.Add(new IbanData("FR", 27, @"\d{10}[a-zA-Z0-9]{11}\d\d", true, "FR1420041010050500013M02606"));
                newList.Add(new IbanData("GE", 22, @"[a-zA-Z09]{2}\d{16}", false, "GE29NB0000000101904917"));
                newList.Add(new IbanData("GB", 22, @"[A-Z]{4}\d{14}", true, "GB29NWBK60161331926819"));
                newList.Add(new IbanData("GI", 23, @"[A-Z]{4}[a-zA-Z0-9]{15}", true, "GI75NWBK000000007099453"));
                newList.Add(new IbanData("GL", 18, @"\d{14}", false, "GL8964710001000206"));
                newList.Add(new IbanData("GR", 27, @"\d{7}[a-zA-Z0-9]{16}", true, "GR1601101250000000012300695"));
                newList.Add(new IbanData("HR", 21, @"\d{17}", false, "HR1210010051863000160"));
                newList.Add(new IbanData("HU", 28, @"\d{24}", true, "HU42117730161111101800000000"));
                newList.Add(new IbanData("IE", 22, @"[A-Z]{4}\d{14}", true, "IE29AIBK93115212345678"));
                newList.Add(new IbanData("IL", 23, @"\d{19}", false, "IL620108000000099999999"));
                newList.Add(new IbanData("IS", 26, @"\d{22}", true, "IS140159260076545510730339"));
                newList.Add(new IbanData("IT", 27, @"[A-Z]\d{10}[a-zA-Z0-9]{12}", true, "IT60X0542811101000000123456"));
                newList.Add(new IbanData("XK", 20, @"\d{16}", false, "XK281500000000000053"));
                newList.Add(new IbanData("LB", 28, @"\d{4}[a-zA-Z0-9]{20}", false, "LB62099900000001001901229114"));
                newList.Add(new IbanData("LI", 21, @"\d{5}[a-zA-Z0-9]{12}", true, "LI21088100002324013AA"));
                newList.Add(new IbanData("LT", 20, @"\d{16}", true, "LT121000011101001000"));
                newList.Add(new IbanData("LU", 20, @"\d{3}[a-zA-Z0-9]{13}", true, "LU280019400644750000"));
                newList.Add(new IbanData("LV", 21, @"[A-Z]{4}[a-zA-Z0-9]{13}", true, "LV80BANK0000435195001"));
                newList.Add(new IbanData("MC", 27, @"\d{10}[a-zA-Z0-9]{11}\d\d", true, "MC1112739000700011111000h79"));
                newList.Add(new IbanData("ME", 22, @"\d{18}", false, "ME25505000012345678951"));
                newList.Add(new IbanData("MK", 19, @"\d{3}[a-zA-Z0-9]{10}\d\d", false, "MK07300000000042425"));
                newList.Add(new IbanData("MT", 31, @"[A-Z]{4}\d{5}[a-zA-Z0-9]{18}", true, "MT84MALT011000012345MTLCAST001S"));
                newList.Add(new IbanData("MD", 24, @"[a-zA-Z0-9]{20}", false, "MD24AG000225100013104168"));
                newList.Add(new IbanData("MU", 30, @"[A-Z]{4}\d{19}[A-Z]{3}", false, "MU17BOMM0101101030300200000MUR"));
                newList.Add(new IbanData("NL", 18, @"[A-Z]{4}\d{10}", true, "NL91ABNA0417164300"));
                newList.Add(new IbanData("NO", 15, @"\d{11}", true, "NO9386011117947"));
                newList.Add(new IbanData("PL", 28, @"\d{8}[a-zA-Z0-9]{16}", true, "PL27114020040000300201355387"));
                newList.Add(new IbanData("PT", 25, @"\d{21}", true, "PT50000201231234567890154"));
                newList.Add(new IbanData("RO", 24, @"[A-Z]{4}[a-zA-Z0-9]{16}", true, "RO49AAAA1B31007593840000"));
                newList.Add(new IbanData("RS", 22, @"\d{18}", false, "RS35260005601001611379"));
                newList.Add(new IbanData("SA", 24, @"\d{2}[a-zA-Z0-9]{18}", false, "SA0380000000608010167519"));
                newList.Add(new IbanData("SE", 24, @"\d{20}", true, "SE4550000000058398257466"));
                newList.Add(new IbanData("SI", 19, @"\d{15}", true, "SI56191000000123438"));
                newList.Add(new IbanData("SK", 24, @"\d{20}", true, "SK3112000000198742637541"));
                newList.Add(new IbanData("SM", 27, @"[A-Z]\d{10}[a-zA-Z0-9]{12}", false, "SM86U0322509800000000270100"));
                newList.Add(new IbanData("TN", 24, @"\d{20}", false, "TN5914207207100707129648"));
                newList.Add(new IbanData("TR", 26, @"\d{5}[a-zA-Z0-9]{17}", false, "TR330006100519786457841326"));

                return newList;
            }
        }

        public List<iBanBicModel> BicCountry(string id)
        {

            id = id.Replace(" ", "");

            string bankCode = string.Empty;
            string branchCode = string.Empty;
            var isoCode = id.Substring(0, 2).ToUpper();

            switch (isoCode)
            {
                case "AL": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); branchCode = id.Remove(0, 7).Remove(5, id.Length - 12); break;
                case "AD": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); branchCode = id.Remove(0, 8).Remove(4, id.Length - 12); break;
                case "AT": bankCode = id.Remove(0, 4).Remove(5, id.Length - 9); break;
                case "AZ": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "BH": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "BE": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); break;
                case "BA": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); break;
                case "BR": bankCode = id.Remove(0, 4).Remove(7, id.Length - 11); break;
                case "BG": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "HR": bankCode = id.Remove(0, 4).Remove(7, id.Length - 11); break;
                case "CY": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); branchCode = id.Remove(0, 7).Remove(5, id.Length - 12); break;
                case "CZ": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "DK": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "EE": bankCode = id.Remove(0, 4).Remove(2, id.Length - 6); break;
                case "FO": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "FI": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); break;
                case "FR": bankCode = id.Remove(0, 4).Remove(5, id.Length - 9); break;
                case "GE": bankCode = id.Remove(0, 4).Remove(2, id.Length - 6); break;
                case "DE": bankCode = id.Remove(0, 4).Remove(8, id.Length - 12); break;
                case "GI": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "GR": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); break;
                case "GL": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "HU": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); branchCode = id.Remove(0, 7).Remove(5, id.Length - 12); break;
                case "IS": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "IE": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "IL": bankCode = id.Remove(0, 4).Remove(2, id.Length - 6); branchCode = id.Remove(0, 6).Remove(3, id.Length - 9); break;
                case "IT": bankCode = id.Remove(0, 4).Remove(5, id.Length - 11); branchCode = id.Remove(0, 11).Remove(5, id.Length - 16); break;
                case "XK": bankCode = id.Remove(0, 4).Remove(2, id.Length - 6); branchCode = id.Remove(0, 6).Remove(2, id.Length - 8); break;
                case "LV": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "LI": bankCode = id.Remove(0, 4).Remove(5, id.Length - 9); break;
                case "LT": bankCode = id.Remove(0, 4).Remove(5, id.Length - 9); break;
                case "LU": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); break;
                case "MK": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); break;
                case "MT": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "MD": bankCode = id.Remove(0, 4).Remove(2, id.Length - 6); break;
                case "MC": bankCode = id.Remove(0, 4).Remove(5, id.Length - 9); break;
                case "ME": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); break;
                case "NL": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "NO": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "PL": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); break;
                case "PT": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "RO": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "SM": bankCode = id.Remove(0, 5).Remove(4, id.Length - 9); break;
                case "RS": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); break;
                case "SK": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "SI": bankCode = id.Remove(0, 4).Remove(2, id.Length - 6); break;
                case "ES": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
                case "SE": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); break;
                case "CH": bankCode = id.Remove(0, 4).Remove(5, id.Length - 9); branchCode = id.Remove(0, 9).Remove(4, id.Length - 13); break;
                case "TR": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); branchCode = id.Remove(0, 8).Remove(5, id.Length - 13); break;
                case "AE": bankCode = id.Remove(0, 4).Remove(3, id.Length - 7); break;
                case "GB": bankCode = id.Remove(0, 4).Remove(4, id.Length - 8); break;
            }

            if (isoCode == "GB")
            {
                var sortCode = id.Remove(0, 8).Remove(6, id.Length - 14);

                using (var context = new ApplicationDbContext())
                {
                    //var bicCode = new SqlParameter("SortCode", sortCode);
                    var result = context.Database.SqlQuery<iBanBicModel>("GetFullDetailsBicCountry2 @SortCode", new SqlParameter("SortCode", sortCode)).ToList();
                    return result;
                }
            }

            if (branchCode != "")
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = context.Database.SqlQuery<iBanBicModel>("GetFullDetailsBankPlus2 @BankCode, @IsoCode, @BranchCode", new SqlParameter("BankCode", bankCode), new SqlParameter("IsoCode", isoCode), new SqlParameter("BranchCode", branchCode)).ToList();
                    if (result.Count == 0)
                    {
                        result = context.Database.SqlQuery<iBanBicModel>("GetFullDetailsBankBranch2 @BankCode, @IsoCode", new SqlParameter("BankCode", bankCode), new SqlParameter("IsoCode", isoCode)).ToList();
                    }

                    return result;
                }
            }
            else
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = context.Database.SqlQuery<iBanBicModel>("GetFullDetailsBank2 @BankCode, @IsoCode", new SqlParameter("BankCode", bankCode), new SqlParameter("IsoCode", isoCode)).ToList();
                    return result;
                }
            }
        }
    }
}