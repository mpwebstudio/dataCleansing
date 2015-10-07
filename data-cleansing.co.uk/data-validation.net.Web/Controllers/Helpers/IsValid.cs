using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Controllers.Helpers
{
    public class IsValid
    {
        public bool ValidUser(string apiNumber, string keyWord, string service)
        {
            var getUserIp = new Helpers.GetUserIP();
            var ipAddress = getUserIp.GetclientIP();

            string host = string.Empty;
            string ipNumber = string.Empty;

            bool tUser = false;

            var helper = new Helpers.CreditsDeduct();
            foreach (var ipResult in ipAddress)
            {

                if (ipResult.Host == "data-cleansing.net")
                {
                    tUser = helper.CheckTemporaryUser(ipResult.IP, 1);
                }
                else
                {
                    host = ipResult.Host;
                    ipNumber = ipResult.IP;

                    tUser = helper.IsValidApiUser(ipResult.IP, ipResult.Host, apiNumber, 6, keyWord, "single", service);
                }
                break;
            }

            return tUser;
        }
    }
}