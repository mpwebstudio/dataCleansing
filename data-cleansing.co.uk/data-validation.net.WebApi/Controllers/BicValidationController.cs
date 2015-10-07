namespace data_validation.net.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using data_cleansing.net.Data;
    using data_validation.net.Web.ViewModels;
    using System.Data.SqlClient;

    public class BicValidationController : BaseApiController
    {
        public IHttpActionResult Get(string id)
        {
            var bic = new data_validation.net.Web.Controllers.Helpers.RemoveSpecialCharacters();
            var cleanBic = bic.RemoveSpecialCharactersBasic(id);

            var userIp = Request.GetOwinContext().Request.RemoteIpAddress;
            var userHost = Request.GetOwinContext().Request.Host.Value.ToString();

            var isFraud = new IsFraudController();
            var isFraudUser = isFraud.Check(User.Identity.Name, userIp, userHost);

            if (isFraudUser)
            {
                var errorMessage = new List<BicInfoModel>();
                errorMessage.Add(new BicInfoModel { IsValid = "Fraud Triger On!" });
                return Ok(errorMessage);
            }

            var helper = new data_validation.net.Web.Controllers.Helpers.CreditsDeduct();
            var hasCredits = helper.IsValid(User.Identity.Name, 6, cleanBic, "singleAPI", "swift");

            if (!hasCredits)
            {
                var errorMessage = new List<BicInfoModel>();
                errorMessage.Add(new BicInfoModel { IsValid = "No Credits Left" });
                return Ok(errorMessage);
            }

            using (var context = new ApplicationDbContext())
            {
                var result = context.Database.SqlQuery<BicInfoModel>("GetFullDetailsSwift2 @SwiftCode", new SqlParameter("SwiftCode", cleanBic)).ToList();
                
                return Ok(result);
            }
        }
    }
}