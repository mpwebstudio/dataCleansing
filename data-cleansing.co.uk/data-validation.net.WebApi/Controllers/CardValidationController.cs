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

    public class CardValidationController : BaseApiController
    {
        public IHttpActionResult Get(string id)
        {
            var card = new data_validation.net.Web.Controllers.Helpers.RemoveSpecialCharacters();
            var cleanNumber = card.RemoveSpecialCharactersCard(id);

            var userIp = Request.GetOwinContext().Request.RemoteIpAddress;
            var userHost = Request.GetOwinContext().Request.Host.Value.ToString();

            var isFraud = new IsFraudController();
            var isFraudUser = isFraud.Check(User.Identity.Name, userIp, userHost);

            if (isFraudUser)
            {
                var errorMessage = new List<CardValidationViewModel>();
                errorMessage.Add(new CardValidationViewModel { IsValid = "Fraud Triger On!" });
                return Ok(errorMessage);
            }

            var helper = new data_validation.net.Web.Controllers.Helpers.CreditsDeduct();
            var hasCredits = helper.IsValid(User.Identity.Name, 6, cleanNumber, "singleAPI", "card");

            if (!hasCredits)
            {
                var errorMessage = new List<CardValidationViewModel>();
                errorMessage.Add(new CardValidationViewModel { IsValid = "No Credits Left" });
                return Ok(errorMessage);
            }

            var cardType = new CardType();
            var result = cardType.CardLogic(cleanNumber);

            return Ok(result);
        }
    }
}