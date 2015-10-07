namespace data_validation.net.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using data_cleansing.net.Data;
    using data_validation.net.Web.Models.ViewModels;

    [Authorize]
    public class EmailValidationController : BaseApiController
    {
        public IHttpActionResult Get(string id)
        {
            var cleanEmail = new data_validation.net.Web.Controllers.Helpers.RemoveSpecialCharacters();
            var emailAddress = cleanEmail.RemoveSpecialCharactersEmail(id);

            var userIp = Request.GetOwinContext().Request.RemoteIpAddress;
            var userHost = Request.GetOwinContext().Request.Host.Value.ToString();

            var isFraud = new IsFraudController();
            var isFraudUser = isFraud.Check(User.Identity.Name, userIp, userHost);

            if (isFraudUser)
            {
                var errorMessage = new List<EmailValidationViewModel>();
                errorMessage.Add(new EmailValidationViewModel { IsValid = "Fraud Triger On!" });
                return Ok(errorMessage);
            }

            var helper = new data_validation.net.Web.Controllers.Helpers.CreditsDeduct();
            var hasCredits = helper.IsValid(User.Identity.Name, 6, emailAddress, "singleAPI", "email");

            if (!hasCredits)
            {
                var errorMessage = new List<EmailValidationViewModel>();
                errorMessage.Add(new EmailValidationViewModel { IsValid = "No Credits Left" });
                return Ok(errorMessage);
            }

            var email = new data_validation.net.Web.Controllers.Helpers.Email();
            var emailResult = email.Validate(emailAddress);

            return Ok(emailResult);
        }
    }
}