using data_validation.net.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace data_validation.net.Web.Controllers.Helpers
{
    public class CardType
    {
        public class StatusCard
        {
            public bool IsValid { get; set; }

            public string Type { get; set; }

            public StatusCard(bool isValid, string type)
            {
                IsValid = isValid;
                Type = type;
            }
        }

        public StatusCard CheckStatus(string cardNumber)
        {
            Regex regVisa = new Regex("^4[0-9]{12}(?:[0-9]{3})?$");
            Regex regMaster = new Regex("^5[1-5][0-9]{14}$");
            Regex regExpress = new Regex("^3[47][0-9]{13}$");
            Regex regDiners = new Regex("^3(?:0[0-5]|[68][0-9])[0-9]{11}$");
            Regex regDiscover = new Regex("^6(?:011|5[0-9]{2})[0-9]{12}$");
            Regex regJCB = new Regex("^(?:2131|1800|35\\d{3})\\d{11}$");
            
            var result = 0;

            for (int i = 0; i < cardNumber.Length; i++)
            {
                if (i % 2 == 0)
                {
                    result = result + (Int32.Parse(cardNumber[i].ToString()) * 2);
                }
                else
                {
                    result = result + (Int32.Parse(cardNumber[i].ToString()) * 2);
                }
            }

            if (result % 10 == 0)
            {

                if (regVisa.IsMatch(cardNumber))
                {

                    return new StatusCard(true, "visa");
                }
                else if (regMaster.IsMatch(cardNumber))
                {
                    return new StatusCard(true, "mastercard");
                }
                else if (regExpress.IsMatch(cardNumber))
                {
                    return new StatusCard(true, "amex");
                }
                else if (regDiscover.IsMatch(cardNumber))
                {
                    return new StatusCard(true, "discovers");
                }
                else
                {
                    return new StatusCard(false, "invalid");
                }
            }
            else
            {
                return new StatusCard(false, "invalid");
            }

        }

        public List<CardValidationViewModel> CardLogic(string id)
        {
            id = id.Replace(" ", "");

            //Issue by Visa / MasterCard ...

            string cardNumber = id;

            // Card validation 13 - 16 digit

            char compare = id[id.Length - 1];

            id = id.Remove(id.Length - 1);

            char[] charArray = id.ToCharArray();

            Array.Reverse(charArray);

            id = new string(charArray);

            int[] u = id.Select(x => Convert.ToInt32(x) - 48).ToArray();

            for (int i = 0; i < u.Length; i++)
            {
                if (i % 2 == 0)
                {
                    u[i] = u[i] * 2;
                }

                if (u[i] > 9)
                {
                    u[i] = u[i] - 9;
                }
            }

            int rest = u.Sum() * 9;

            string res = rest.ToString();

            char against = res[2];

            string answer = "Card is not valid";

            if (char.Equals(against, compare))
            {
                answer = "Card is valid";
            }

            var result = new List<CardValidationViewModel>();

            result.Add(new CardValidationViewModel
            {
                CardIssue = this.IssueBy(cardNumber),
                IsValid = answer,
                CardNumber = cardNumber
            });

            return result;
        }

        private string IssueBy(string id)
        {
            string first = "non";

            string issueBy = "non";

            for (int i = 1; i < 5; i++)
            {
                first = id.Substring(0, i);

                switch (first)
                {
                    case "4": issueBy = "Visa"; break;
                    case "51": issueBy = "MasterCard"; break;
                    case "52": issueBy = "MasterCard"; break;
                    case "53": issueBy = "MasterCard"; break;
                    case "54": issueBy = "MasterCard"; break;
                    case "55": issueBy = "MasterCard"; break;
                    case "5018": issueBy = "Maestro"; break;
                    case "5020": issueBy = "Maestro"; break;
                    case "5038": issueBy = "Maestro"; break;
                    case "5893": issueBy = "Maestro"; break;
                    case "6334": issueBy = "Maestro"; break;
                    case "6759": issueBy = "Maestro"; break;
                    case "6761": issueBy = "Maestro"; break;
                    case "6762": issueBy = "Maestro"; break;
                    case "6763": issueBy = "Maestro"; break;
                    case "4026": issueBy = "Visa Electron"; break;
                    case "417500": issueBy = "Visa Electron"; break;
                    case "4508": issueBy = "Visa Electron"; break;
                    case "4505": issueBy = "Visa Electron"; break;
                    case "4844": issueBy = "Visa Electron"; break;
                    case "4913": issueBy = "Visa Electron"; break;
                    case "4917": issueBy = "Visa Electron"; break;
                    case "1": issueBy = "UATP"; break;
                    case "5019": issueBy = "Dankort"; break;
                    case "636": issueBy = "InstaPayment"; break;
                    case "637": issueBy = "InstaPayment"; break;
                    case "638": issueBy = "InstaPayment"; break;
                    case "639": issueBy = "InstaPayment"; break;
                    case "6011": issueBy = "Discover"; break;
                    case "644": issueBy = "Discover"; break;
                    case "645": issueBy = "Discover"; break;
                    case "646": issueBy = "Discover"; break;
                    case "647": issueBy = "Discover"; break;
                    case "648": issueBy = "Discover"; break;
                    case "649": issueBy = "Discover"; break;
                    case "65": issueBy = "Discover"; break;
                    case "300": issueBy = "Diners Club Internationa"; break;
                    case "301": issueBy = "Diners Club Internationa"; break;
                    case "302": issueBy = "Diners Club Internationa"; break;
                    case "303": issueBy = "Diners Club Internationa"; break;
                    case "304": issueBy = "Diners Club Internationa"; break;
                    case "305": issueBy = "Diners Club Internationa"; break;
                    case "309": issueBy = "Diners Club Internationa"; break;
                    case "36": issueBy = "Diners Club Internationa"; break;
                    case "38": issueBy = "Diners Club Internationa"; break;
                    case "39": issueBy = "Diners Club Internationa"; break;
                    case "34": issueBy = "Ammerican Express"; break;
                    case "37": issueBy = "Ammerican Express"; break;
                }
            }

            if (issueBy == "non")
            {
                int maestro = Convert.ToInt32(id.Substring(0, 6));

                if (maestro >= 500000 && maestro <= 509999 || maestro >= 560000 && maestro <= 699999)
                {
                    return issueBy = "Maestro";
                }

                int jcb = Convert.ToInt32(id.Substring(0, 4));

                if (jcb >= 3528 && jcb <= 3589)
                {
                    return issueBy = "JCB";
                }

                int discoverCard = Convert.ToInt32(id.Substring(0, 6));

                if (discoverCard >= 622126 && discoverCard <= 622925)
                {
                    return issueBy = "Discover";
                }
            }

            return issueBy;
        }


    }
}