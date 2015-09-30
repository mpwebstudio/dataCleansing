using data_validation.net.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;


namespace data_validation.net.Web.Controllers.Helpers
{
    public class GetUserIP
    {
        public List<UserIPModel> GetclientIP()
        {
            var returnResult = new List<UserIPModel>();
            string result = string.Empty;
            string functionReturnValue = string.Empty;
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ip))
            {
                string[] ipRange = ip.Split(',');
                int le = ipRange.Length - 1;
                result = ipRange[0];
            }
            else
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            
            if ((((System.Web.HttpContext.Current.Request.UrlReferrer) != null)))
            {
                functionReturnValue = HttpContext.Current.Request.UrlReferrer.Host.ToString();
            }

            var esult = HttpContext.Current.Request.UrlReferrer.Authority.ToString();

            returnResult.Add(new UserIPModel
                {
                    IP = result,
                    Host = functionReturnValue,
                    Remote = esult
                });

            return returnResult;
        }
    }
}