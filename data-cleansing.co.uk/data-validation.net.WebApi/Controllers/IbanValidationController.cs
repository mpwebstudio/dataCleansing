namespace data_validation.net.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using data_cleansing.net.Data;
    using data_validation.net.Web.ViewModels;
    using data_validation.net.Web.Controllers.Helpers;
    

    public class IbanValidationController : BaseApiController
    {
        public IHttpActionResult Get(string id)
        {
            var iban = new data_validation.net.Web.Controllers.Helpers.RemoveSpecialCharacters();
            var cleanIban = iban.RemoveSpecialCharactersBasic(id);

            var userIp = Request.GetOwinContext().Request.RemoteIpAddress;
            var userHost = Request.GetOwinContext().Request.Host.Value.ToString();

            var isFraud = new IsFraudController();
            var isFraudUser = isFraud.Check(User.Identity.Name, userIp, userHost);

            if (isFraudUser)
            {
                var errorMessage = new List<iBanBicModel>();
                errorMessage.Add(new iBanBicModel { IsValid = "Fraud Triger On!" });
                return Ok(errorMessage);
            }

            var helper = new data_validation.net.Web.Controllers.Helpers.CreditsDeduct();
            var hasCredits = helper.IsValid(User.Identity.Name, 6, cleanIban, "singleAPI", "iban");

            if (!hasCredits)
            {
                var errorMessage = new List<iBanBicModel>();
                errorMessage.Add(new iBanBicModel { IsValid = "No Credits Left" });
                return Ok(errorMessage);
            }

            var resT = new BankValidationController();

            var temp = BankValidationController.Iban.CheckIban(id, true);

            var error = new List<iBanBicModel>();

            if (!temp.IsValid)
            {
                error.Add(new iBanBicModel { IsValid = temp.Message, BankCode = cleanIban });

                return Ok(error);
            }

            var result = resT.BicCountry(cleanIban.ToUpper());

            return Ok(result);
        }
    }
}