using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace data_validation.net.Web.Controllers.Helpers
{
    public class RemoveSpecialCharacters
    {
        public string RemoveCharactersTelephone(string str)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                }
            }

            var id = sb.ToString();

            if (id == "")
            {
                id = "0";
                return id;
            }

            if (id.ToString().Substring(0, 4) == "0044")
            {
                sb2.Append("0" + id.Substring(4, id.Length - 4));
                return sb2.ToString();
            }
            else if (id.ToString().Substring(0, 3) == "044")
            {
                sb2.Append("0" + id.Substring(3, id.Length - 3));
                return sb2.ToString();
            }
            else if (id.Substring(0, 2) == "44")
            {
                sb2.Append("0" + id.Substring(2, id.Length - 2));
                return sb2.ToString();
            }

            return id;
        }

        public string RemoveSpecialCharactersAddress(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ' || c == ',' || c == ';')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public string RemoveSpecialCharactersEmail(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ' || c == '_' || c == '-' || c == '@' || c == '.')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public string RemoveSpecialCharactersBasic(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ' || c == ',' || c == ';')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public string RemoveSpecialCharactersCard(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (c >= '0' && c <= '9')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}