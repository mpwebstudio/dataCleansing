namespace data_validation.net.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using data_cleansing.net.Data;
    using data_validation.net.Web.ViewModels.DataCleansing;

    [Authorize]
    public class AddressValidationController : BaseApiController
    {
        public IHttpActionResult Get(string id)
        {
            var cleanAddress = new data_validation.net.Web.Controllers.Helpers.RemoveSpecialCharacters();
            var address = cleanAddress.RemoveSpecialCharactersAddress(id);

            var userIp = Request.GetOwinContext().Request.RemoteIpAddress;
            var userHost = Request.GetOwinContext().Request.Host.Value.ToString();

            var isFraud = new IsFraudController();
            var isFraudUser = isFraud.Check(User.Identity.Name, userIp, userHost);

            if (isFraudUser)
            {
                var errorMessage = new List<AddressModel>();
                errorMessage.Add(new AddressModel { IsValid = "Fraud Triger On!" });
                return Ok(errorMessage);
            }

            var helper = new data_validation.net.Web.Controllers.Helpers.CreditsDeduct();
            var res = helper.IsValid(User.Identity.Name, 6, address, "singleAPI", "address");

            if (res == false)
            {
                var errorMessage = new List<AddressModel>();
                errorMessage.Add(new AddressModel { IsValid = "No Credits Left" });
                return Ok(errorMessage);
            }

            var dataCleansingLogic = new data_validation.net.Web.Controllers.Helpers.DataCleansingLogich();
            var validAddress = dataCleansingLogic.Cleansing(address);

            return Ok(validAddress);

        }
    }
}