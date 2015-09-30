namespace data_validation.net.Web.Controllers.DataCleansing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    using data_validation.net.Web.Controllers.Helpers;
    using data_validation.net.Web.Models.DataCleansing;
    using data_cleansing.net.Data;
    using System.Data.SqlClient;
    using data_cleansing.net.Models;
    public class DataCleansingLogich : BaseController
    {
        static string patern = "^(GIR 0AA)|((([A-Z][0-9]{1,2})|(([A-Z][A-HJ-Y][0-9]{1,2})|(([A-Z][0-9][A-Z])|([A-Z][A-HJ-Y][0-9]?[A-Z])))) [0-9][A-Z]{2})$";

        public List<AddressModel> Cleansing(string id)
        {
            var resT = new StoreProcedure();
            id = id.Replace("\r", string.Empty).Replace("\n", string.Empty).ToUpper();
            string[] res = new string[] { };
            string[] addressSearch = new string[] { };
            var postCode = string.Empty;
            var temporaryPostCode = string.Empty;
            var houseNumber = string.Empty;
            string street = string.Empty;
            string post = string.Empty;
            string numb = string.Empty;
            string flat = string.Empty;

            if (id.Contains(',') == true)
            {
                res = id.Split(',');
                addressSearch = id.Split(',');

                for (int i = 0; i < addressSearch.Count(); i++)
                {
                    res[i] = res[i].TrimStart().TrimEnd();
                    addressSearch[i] = addressSearch[i].TrimStart().TrimEnd();
                    if (res[i].ToUpper().Contains("FLAT"))
                    {
                        flat = res[i];
                    }
                }
            }
            else if (id.Contains(';') == true)
            {
                res = id.Split(';');
                addressSearch = id.Split(';');

                for (int i = 0; i < addressSearch.Count(); i++)
                {
                    res[i] = res[i].TrimStart().TrimEnd();
                    addressSearch[i] = addressSearch[i].TrimStart().TrimEnd();
                    if (res[i].ToUpper().Contains("FLAT"))
                    {
                        flat = res[i];
                    }
                }
            }
            else
            {
                res = id.Split(' ');
                addressSearch = id.Split(' ');
                //Слагам тука
            }
            for (int i = 0; i < addressSearch.Count(); i++)
            {
                try
                {
                    if (res[i].ToUpper().Contains("FLAT") && flat == "")
                    {
                        flat = res[i] + " " + res[i + 1];
                    }

                    if (res[i].Length > 2)
                    {
                        if (PostCodeType.postCodeType.Contains(res[i].Substring(0, 3)))
                        {
                            post = res[i];
                            if (res[i].Length < 5 && i <= addressSearch.Length - 1)
                            {
                                if (res[i + 1].Length == 3)
                                {
                                    string firstCharNext = res[i + 1].Substring(0, 1);
                                    int n;
                                    bool isNumeric = int.TryParse(firstCharNext, out n);
                                    string secondChar = res[i + 1].Substring(1, 1);
                                    int t;
                                    bool isChar = int.TryParse(secondChar, out t);
                                    if (isNumeric == true && isChar == false)
                                    {
                                        post = res[i] + " " + res[i + 1];
                                    }
                                }
                            }
                            else if (res[i].Length > 4)
                            {
                                var checkChar = res[i].Substring(res[i].Length - 3, 1);
                                if (checkChar != " ")
                                {
                                    res[i] = res[i].Insert(res[i].Length - 3, " ");
                                    Match postCo = Regex.Match(res[i], patern);
                                    if (postCo.Success)
                                        post = res[i];
                                }
                            }
                        }
                        else
                        {
                            int n;
                            bool isNumeric = int.TryParse(res[i], out n);
                            if (isNumeric)
                            {
                                numb = res[i];
                            }
                        }
                    }
                    else
                    {
                        int n;
                        bool isNumeric = int.TryParse(res[i], out n);
                        if (isNumeric)
                        {
                            numb = res[i];
                        }
                    }
                }
                catch
                {

                }
            }

            if (post.Length == 0)
            {
                for (int i = 0; i < addressSearch.Length - 1; i++)
                {
                    string tempPostCode = addressSearch[i] + addressSearch[i + 1];
                    if (tempPostCode.Length > 5)
                    {
                        tempPostCode = tempPostCode.Insert(tempPostCode.Length - 3, " ");
                        Match regPostCode = Regex.Match(tempPostCode, patern);
                        if (regPostCode.Success && PostCodeType.postCodeType.Contains(tempPostCode.Substring(0, 3)))
                        {
                            post = tempPostCode;
                            break;
                        }
                    }
                }

                for (int i = 0; i < addressSearch.Length; i++)
                {
                    string ifPostCodeOneWord = addressSearch[i].Replace(" ", "");
                    if (ifPostCodeOneWord.Length > 5)
                    {
                        ifPostCodeOneWord = ifPostCodeOneWord.Insert(ifPostCodeOneWord.Length - 3, " ");
                        Match regpostCode = Regex.Match(ifPostCodeOneWord, patern);
                        if (regpostCode.Success && PostCodeType.postCodeType.Contains(ifPostCodeOneWord.Insert(ifPostCodeOneWord.Length - 3, " ").Substring(0, 3)))
                        {
                            post = ifPostCodeOneWord;
                            break;
                        }
                    }
                }
            }

            if (post.Length >= 4 && numb.Length > 0)
            {



                var searchStreet = FoundStreet(addressSearch, post);

                if (searchStreet.Count() >= 1)
                {
                    foreach (var item in searchStreet)
                    {
                        if (item.postCode == "" || item.postCode == null)
                        {
                            street = item.street;
                            houseNumber = item.houseNumber;
                            postCode = post;

                            if (PostCodeType.postCodeType.Contains(postCode.Replace(" ", "").Insert(postCode.Length - 3, " ").Substring(0, 3)))
                                goto Address;
                        }
                        else
                        {
                            if (PostCodeType.postCodeType.Contains(item.postCode.Replace(" ", "").Insert(item.postCode.Length - 3, " ").Substring(0, 3)))
                                post = item.postCode;
                            numb = item.houseNumber;
                        }
                    }
                }



                if (flat.Length > 0)
                {

                    var resultPlusFlat = resT.FullDetailsPlusFlat(post, numb, flat);

                    if (resultPlusFlat.Count > 0)
                    {
                        return resultPlusFlat;
                    }
                }

                if (post.Replace(" ", "").Length >= 5)
                {
                    var result = resT.FullDetailsPlusStreet(post, numb);

                    return result;
                }
            }


            Match postCodeOnly = Regex.Match(post, patern);
            if (postCodeOnly.Success && PostCodeType.postCodeType.Contains(post.Substring(0, 3)))
            {
                var result = resT.FullDetails(post);

                return result;
            }
            // Махам от тука  
            // }


            for (int i = 0; i < res.Count(); i++)
            {
                //Премахваме всякакви шантави знаци
                res[i] = Regex.Replace(res[i], "[^0-9A-Za-z]+", "");
                if (res[i].Count() == 6 || res[i].Count() == 7)
                {   //Слагам разделителя
                    res[i] = res[i].Insert(res[i].Count() - 3, " ");

                    //REGEX да проверя кое е пощенският код
                    Match match = Regex.Match(res[i], patern);
                    if (match.Success)
                    {   //втора проверка дали първият знак от втората част е цифра
                        var cha = res[i].Substring(res[i].Count() - 3, 1);
                        int n;
                        bool isNumeric = int.TryParse(cha, out n);
                        if (isNumeric)
                        {
                            postCode = res[i];
                        }
                    }
                }
            }

            //Проверяваме ако няма намерен пощенски код
            if (postCode.Count() == 0)
            {
                for (int i = 0; i < res.Count(); i++)
                {
                    try
                    {
                        if (PostCodeType.postCodeType.Contains(res[i].Substring(0, 3)))
                        {
                            res[i] = res[i].Replace(" ", "");

                            for (int j = 1; j < res[i].Count(); j++)
                            {
                                string cha = res[i].Substring(res[i].Count() - j, 1);
                                int n;
                                bool isNumeric = int.TryParse(cha, out n);
                                if (isNumeric)
                                {
                                    res[i] = res[i].Insert(res[i].Length - j, " ");
                                    break;
                                }

                            }
                            postCode = res[i];
                            break;
                        }
                    }
                    catch
                    {

                    }
                }
            }

            var foundStreet = FoundStreet(addressSearch, post);

            if (foundStreet.Count() == 0 && postCode != "" && postCode != null)
            {
                var streetNumber = StreetNumber(addressSearch, postCode);

                if (streetNumber.Length > 0)
                {
                    var result = resT.FullDetailsPlusStreet(postCode, streetNumber);

                    return result;

                }
            }

            foreach (var item in foundStreet)
            {
                street = item.street;
                houseNumber = item.houseNumber;
                if (item.postCode != null)
                {
                    postCode = item.postCode;
                    if (street == null)
                    {
                        //this has to be list, because of double house numbers like 42A, 42B etc..

                        var result = resT.FullDetailsPlusStreet(postCode, houseNumber);

                        return result;
                    }
                }
            }

            //Проверявам дали съм намерил пощенски код

            Address:

            //при намерен пощенски код
            if (postCode.Count() > 0 && postCode.Length > 5)
            {
                //Вземаме данните от базата, чрез процедурата GetFullDetails и ги прехвътлям в лист за да мога да ги обработя повеяе от веднъж при нужда
                var data = resT.FullDetailsFirstOrDefault(postCode);


                //ако улицата от базата съвпада с улицата от записа
                if (data.Street == street.ToUpper())
                {
                    var result = new List<AddressModel>();

                    result.Add(new AddressModel
                    {
                        AdministrativeCounty = data.AdministrativeCounty,
                        BuildingName = data.BuildingName,
                        BuildingNumber = houseNumber,
                        City = data.City.Replace("u0027", " "),
                        Flat = data.Flat,
                        Locality = data.Locality,
                        PostCode = data.PostCode,
                        Street = data.Street.Replace("u0027", " "),
                        TraditionalCounty = data.TraditionalCounty,
                        OrganisationName = data.OrganisationName.Replace("u0026", "&"),
                        IsValid = "Corrected"
                    });
                    return result;
                }

                //Ако улицата не съвпада с тази от записа в базата
                var streetFound = new List<string>();

                streetFound.Add(data.Street);


                //пускаме един Левенщаин за да видим дали не е само правописна грешка
                var foundWords = FuzzySearch.Search(street, streetFound, 0.8);

                //Ако е правописна грешка
                if (foundWords.Count > 0)
                {
                    var result = resT.FullDetailsPlusStreet(post, numb);

                    return result;

                }
                //Ако не е правописна грешка
                else
                {
                    var fuzzy = new FuzzySearch();

                    var resultPlusOne = fuzzy.MinusLetter(postCode + " ", houseNumber, street);

                    if (resultPlusOne.Count > 0)
                        return resultPlusOne;

                    //If there is white space in street
                    var resultNoWhiteSpace = fuzzy.MinusLetter(postCode + " ", houseNumber, street.Replace(" ", ""));

                    if (resultNoWhiteSpace.Count > 0)
                        return resultNoWhiteSpace;

                    //Вземаме всички улици за пощенските кодове без последната буква
                    var resultMinusOne = fuzzy.MinusLetter(postCode, houseNumber, street);

                    if (resultMinusOne.Count > 0)
                        return resultMinusOne;

                    //Ако няма съвпадение, вземаме всички улици без последните 2 букви
                    string postCodeMinusOne = postCode.Substring(0, postCode.Length - 1);

                    var resultMinusTwo = fuzzy.MinusLetter(postCodeMinusOne, houseNumber, street);

                    if (resultMinusTwo.Count > 0)
                        return resultMinusTwo;

                    //Само първата част на ПК
                    string postCodeMinusTwo = postCodeMinusOne.Substring(0, postCodeMinusOne.Length - 1);

                    var resultMinusThree = fuzzy.MinusLetter(postCodeMinusTwo, houseNumber, street);

                    if (resultMinusThree.Count > 0)
                        return resultMinusThree;

                    if (post.Length > 0 && numb.Length > 0)
                    {
                        var houseNumberPostCode = resT.FullDetailsPlusStreet(post, numb);

                        if (houseNumberPostCode.Count > 0)
                            return houseNumberPostCode;
                    }
                }
            }

            if (post.Length > 0 && street.Length > 0)
            {

                var result = resT.FullDetailsPartialStreet(post, street);

                return result;

            }

            var resultT = new List<AddressModel>();
            resultT.Add(new AddressModel()
            {
                Flat = id,
                PostCode = "No match",
                IsValid = "No match found"
            });
            return resultT;

        }

        public class FoundAddress
        {
            public string houseNumber { get; set; }
            public string street { get; set; }
            public string postCode { get; set; }

        }

        public static List<FoundAddress> FoundStreet(string[] addressSearch, string post)
        {
            StringBuilder streetClient = new StringBuilder();
            //to COMPLEATE!!!!!!!!!!!!!!
            string[] searchItems = { "CL", "STREET", "GARDENS", "CLOSE", "STR", "SQARE", "SQ", "AVE", "AVENUE", "GLYD", "GROVE", "ROAD", "RD", "DRIVE", "VIEW", "PLACE", "COURT", "LANE", "CRESCENT", "WAY", "CLWYD", "TERRACE", "LN" };
            var result = new List<FoundAddress>();
            var addressStreetFound = string.Empty;
            string houseNumber = string.Empty;
            int iterationNumber = -1;

            // Проверявам дали имам запис който съдържа str, street и т.н.
            for (int i = 0; i < addressSearch.Count(); i++)
            {
                int k = 0;
                for (int ii = 0; ii < addressSearch[i].Count(); ii++)
                {
                    //Проверяваме дали започва с цифра и ако да предполагаме, че това е улицата
                    k++;
                    var cha = addressSearch[i].Substring(ii, 1);
                    int n;
                    bool isNumeric = int.TryParse(cha, out n);
                    if (iterationNumber == -1 || iterationNumber == i)
                    {
                        if (isNumeric)
                        {
                            houseNumber = houseNumber + cha;
                            iterationNumber = i;
                            if (addressSearch[i].Length > 5)
                            {
                                streetClient.Append(cha);
                                addressStreetFound = addressSearch[i];

                            }
                        }
                        else if (cha == " " || cha == "-")
                        {
                            houseNumber = houseNumber + cha;
                            if (streetClient.Length > 0)
                                streetClient.Append(cha);
                            iterationNumber = i;
                        }
                        else if (k == 1)
                        {
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            if (streetClient.Length > 0)
            {
                houseNumber = streetClient.ToString();
                string street = addressStreetFound.Replace(streetClient.ToString(), "");

                if (street.Length > 4)
                {
                    string tempStreet = street.Insert(street.Length - 3, " ");
                    Match postCo = Regex.Match(tempStreet, patern);
                    if (postCo.Success)
                    {
                        result.Add(new FoundAddress { postCode = tempStreet, houseNumber = houseNumber.TrimEnd().TrimStart() });
                    }
                }
                else
                {
                    result.Add(new FoundAddress { street = street, houseNumber = houseNumber.TrimEnd().TrimStart() });
                }
            }
            else
            {
                int count = 0;
                for (int i = 0; i < addressSearch.Count(); i++)
                {
                    if (count > 0)
                        break;
                    foreach (var text in searchItems)
                    {
                        bool matchFound = addressSearch[i].Contains(text);

                        if (matchFound)
                        {

                            if (addressSearch[i].Replace(" ", "") == post.Replace(" ", ""))
                            {
                                break;
                            }
                            if (i > 0 && addressSearch[i - 1] + addressSearch[i] == post.Replace(" ", ""))
                            {
                                break;
                            }
                            count++;
                            if (i > 0)
                            {
                                var cha = addressSearch[i - 1];
                                cha = cha.Replace(" ", "");
                                int n;
                                bool isNumeric = int.TryParse(cha, out n);
                                if (isNumeric)
                                {
                                    result.Add(new FoundAddress { street = addressSearch[i], houseNumber = houseNumber });
                                    break;
                                }

                                result.Add(new FoundAddress { street = addressSearch[i - 1] + " " + addressSearch[i], houseNumber = houseNumber });
                                break;
                            }
                            //if match is in first iteration ex: Hillside Avenue
                            if (i == 0)
                            {
                                result.Add(new FoundAddress { street = addressSearch[i], houseNumber = houseNumber });
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static string StreetNumber(string[] addressSearch, string postCode)
        {
            string number = string.Empty;
            foreach (var tnp in addressSearch)
            {
                if (tnp == postCode)
                {
                    continue;
                }
                int n;
                bool isNumeric = int.TryParse(tnp, out n);
                if (isNumeric)
                {
                    number = tnp;
                }
            }
            return number;
        }
    }

    public class FuzzySearch
    {
        public static int LevenshteinDistance(string src, string dest)
        {
            int[,] d = new int[src.Length + 1, dest.Length + 1];
            int i, j, cost;
            char[] str1 = src.ToCharArray();
            char[] str2 = dest.ToCharArray();

            for (i = 0; i <= str1.Length; i++)
            {
                d[i, 0] = i;
            }
            for (j = 0; j <= str2.Length; j++)
            {
                d[0, j] = j;
            }
            for (i = 1; i <= str1.Length; i++)
            {
                for (j = 1; j <= str2.Length; j++)
                {

                    if (str1[i - 1] == str2[j - 1])
                        cost = 0;
                    else
                        cost = 1;

                    d[i, j] =
                        Math.Min(
                            d[i - 1, j] + 1,              // Deletion
                            Math.Min(
                                d[i, j - 1] + 1,          // Insertion
                                d[i - 1, j - 1] + cost)); // Substitution

                    if ((i > 1) && (j > 1) && (str1[i - 1] ==
                        str2[j - 2]) && (str1[i - 2] == str2[j - 1]))
                    {
                        d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
                    }
                }
            }
            return d[str1.Length, str2.Length];
        }

        public static List<string> Search(string word, List<string> wordList, double fuzzyness)
        {
            List<string> foundWords =
                (
                    from s in wordList
                    let levenshteinDistance = LevenshteinDistance(word, s)
                    let length = Math.Max(s.Length, word.Length)
                    let score = 1.0 - (double)levenshteinDistance / length
                    where score > fuzzyness
                    select s
                ).ToList();

            return foundWords;
        }

        public List<AddressModel> MinusLetter(string postCode, string houseNumber, string street)
        {
            var postCodeMinusOne = postCode.Substring(0, postCode.Length - 1);

            using (var db = new data_cleansing.net.Data.ApplicationDbContext())
            {
                //take all streets with in post code like CH5 4Y or even shorter
                var dictionaryMinusOne = (from dataB in db.FullDetail
                                          where dataB.PostCode.StartsWith(postCodeMinusOne)
                                          group dataB by dataB.Street into newGroup
                                          select newGroup);


                var listMinusOne = new List<string>();

                foreach (var item in dictionaryMinusOne)
                {
                    string mm = item.Key.TrimEnd().Substring(0, item.Key.LastIndexOf(" "));
                    //вземам само първата час от записа. Ако е Manbry Cl ще вземе само Manbry
                    listMinusOne.Add(mm);
                }

                try
                {
                    street = street.Substring(0, street.IndexOf(" "));
                }
                catch
                {
                    string[] searchItems = { "CL", "STREET", "GARDENS", "CLOSE", "STR", "SQARE", "SQ", "AVE", "AVENUE", "GLYD", "GROVE", "ROAD", "RD", "DRIVE", "VIEW", "PLACE", "COURT", "LANE", "CRESCENT", "WAY", "CLWYD", "TERRACE", "LN" };

                    foreach (var text in searchItems)
                    {
                        bool matchFound = street.Contains(text);

                        if (matchFound)
                        {
                            street = street.Replace(text, "");
                            break;
                        }
                    }
                }

                var foundStreets = FuzzySearch.Search(street, listMinusOne, 0.80);

                var result = new List<AddressModel>();

                foreach (var iFoundStreet in foundStreets)
                {
                    if (houseNumber.Length > 0)
                    {
                        var newResult = (from dataB in db.FullDetail
                                         where dataB.Street.StartsWith(iFoundStreet) && dataB.PostCode.StartsWith(postCodeMinusOne)
                                         select dataB);

                        var houseNumberStript = Regex.Replace(houseNumber, "[^0-9]+", "");

                        foreach (var iNewResult in newResult)
                        {

                            iNewResult.BuildingName = Regex.Replace(iNewResult.BuildingName, "[^0-9]+", "");
                            iNewResult.BuildingNumber = Regex.Replace(iNewResult.BuildingNumber, "[^0-9]+", "");

                            if (iNewResult.BuildingNumber == houseNumberStript || iNewResult.BuildingName == houseNumberStript)
                            {
                                result.Add(new AddressModel
                                {
                                    AdministrativeCounty = iNewResult.AdministrativeCounty,
                                    BuildingName = iNewResult.BuildingName,
                                    BuildingNumber = iNewResult.BuildingNumber,
                                    City = iNewResult.City.Replace("u0027", " "),
                                    Flat = iNewResult.Flat,
                                    Locality = iNewResult.Locality,
                                    PostCode = iNewResult.PostCode,
                                    Street = iNewResult.Street.Replace("u0027", " "),
                                    OrganisationName = iNewResult.OrganisationName.Replace("u0026", "&"),
                                    TraditionalCounty = iNewResult.TraditionalCounty,
                                    IsValid = "Corrected"
                                });

                                return result;
                            }
                        }
                    }

                    //If no street number has been detect
                    var resultIfNotStreet = (from dataB in db.FullDetail
                                             where dataB.Street.StartsWith(iFoundStreet) && dataB.PostCode.StartsWith(postCodeMinusOne)
                                             select dataB).First();
                    string noNumber = string.Empty;

                    if (houseNumber.Length > 0)
                        noNumber = "no match in Data Base";

                    result.Add(new AddressModel
                    {
                        AdministrativeCounty = resultIfNotStreet.AdministrativeCounty,
                        City = resultIfNotStreet.City.Replace("u0027", " "),
                        Locality = resultIfNotStreet.Locality,
                        PostCode = resultIfNotStreet.PostCode,
                        Street = resultIfNotStreet.Street.Replace("u0027", " "),
                        TraditionalCounty = resultIfNotStreet.TraditionalCounty,
                        OrganisationName = resultIfNotStreet.OrganisationName.Replace("u0026", "&"),
                        BuildingNumber = houseNumber,
                        IsValid = "No number found. Corrected",
                    });
                }
                return result;
            }
        }
    }
}
