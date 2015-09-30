using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace data_validation.net.Web.Controllers.Helpers
{
    public class RemoveSpecialCharacters
    {
        public string RemoveCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                }
            }

            var id = sb.ToString();

            if (id.ToString().Substring(0, 4) == "0044")
            {
                id = id.Substring(0, 1) + id.Substring(4, id.Length - 4);
            }

            if (id.Substring(0, 2) == "44")
            {
                id = "0" + id.Substring(2, id.Length - 2);
            }

            return id;
        }
    }
}