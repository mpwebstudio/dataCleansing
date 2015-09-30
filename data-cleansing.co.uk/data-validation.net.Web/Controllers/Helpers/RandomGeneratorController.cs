using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace data_validation.net.Web.Controllers.Helpers
{
    public class RandomGeneratorController
    {
        private static string RandomString(int size)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var letter = new char[size];

            for (int i = 0; i < size; i++)
            {
                letter[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(letter);

            return finalString;
        }

        public static string GetApiNumber()
        {
            var number = RandomString(16);

            StringBuilder apiNumber = new StringBuilder();
            apiNumber.Append(number.Substring(0, 4));
            apiNumber.Append("-");
            apiNumber.Append(number.Substring(4, 4));
            apiNumber.Append("-");
            apiNumber.Append(number.Substring(8, 4));
            apiNumber.Append("-");
            apiNumber.Append(number.Substring(12, 4));

            return apiNumber.ToString();
        }

        public static string GetUserId()
        {
            var number = RandomString(8);

            return number;
        }
    }
}