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
    using data_validation.net.Web.Controllers.Helpers;
    using data_validation.net.Web.Models.ViewModels;
    using data_cleansing.net.Models;

    public class TelephoneValidationController : BaseApiController
    {
        public IHttpActionResult Get(string id)
        {
            var phone = new data_validation.net.Web.Controllers.Helpers.RemoveSpecialCharacters();
            var cleanPhone = phone.RemoveCharactersTelephone(id);

            var userIp = Request.GetOwinContext().Request.RemoteIpAddress;
            var userHost = Request.GetOwinContext().Request.Host.Value.ToString();

            var isFraud = new IsFraudController();
            var isFraudUser = isFraud.Check(User.Identity.Name, userIp, userHost);

            if (isFraudUser)
            {
                var errorMessage = new List<PhoneValidationViewModel>();
                errorMessage.Add(new PhoneValidationViewModel { IsValid = "Fraud Triger On!" });
                return Ok(errorMessage);
            }

            var helper = new data_validation.net.Web.Controllers.Helpers.CreditsDeduct();
            var hasCredits = helper.IsValid(User.Identity.Name, 6, cleanPhone, "singleAPI", "telephone");

            if (!hasCredits)
            {
                var errorMessage = new List<PhoneValidationViewModel>();
                errorMessage.Add(new PhoneValidationViewModel { IsValid = "No Credits Left" });
                return Ok(errorMessage);
            }

            var result = new List<PhoneValidationViewModel>();

            if (cleanPhone.Length != 11 && cleanPhone.Length != 10)
            {
                result.Add(new PhoneValidationViewModel
                {
                    IsValid = "Phone is invalid",
                    Number = cleanPhone
                });

                return Ok(result);
            }

            using (var context = new ApplicationDbContext())
            {
                var tmpData = context.Database.SqlQuery<GetPhoneArea_Result>("GetPhoneArea2 @code", new SqlParameter("code", cleanPhone)).First();

                if (tmpData.IsValid == "Valid")
                {
                    result.Add(new PhoneValidationViewModel
                    {
                        Area = tmpData.AreaCovered,
                        Number = cleanPhone,
                        IsValid = tmpData.IsValid
                    });

                    return Ok(result);
                }
            }
            result.Add(new PhoneValidationViewModel
            {
                Number = cleanPhone,
                IsValid = "Phone seems invalid"
            });

            return Ok(result);
        }
    }
}