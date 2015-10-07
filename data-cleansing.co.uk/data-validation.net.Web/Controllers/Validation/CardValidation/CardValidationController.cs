namespace data_validation.net.Web.Controllers.CardValidation
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Mvc;

    using data_validation.net.Web.Controllers.Helpers;
    using data_validation.net.Web.Models.ViewModels;

    public class CardValidationController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public JsonResult CardValidation(string id, string id2)
        {
            var res = new List<CardValidationViewModel>();

            try
            {
                string number = id.Replace(" ", "");
                long intNumber = Int64.Parse(number);
            }
            catch
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(403, JsonRequestBehavior.AllowGet);
            }

            if(User.Identity.IsAuthenticated)
            {
                var helper = new Helpers.CreditsDeduct();
                var isValid = helper.IsValid(User.Identity.Name, 6, id, "single", "card");

                if (isValid == false)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(404, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                string apiNumber = id2;
                var isValid = new IsValid();
                bool tUser = isValid.ValidUser(apiNumber, id, "address");

                if (tUser == false)
                {
                    res.Add(new CardValidationViewModel { Message = "You have reach your free daily limits!" });
                    return Json(res, JsonRequestBehavior.AllowGet);
                }

            }
            var cardType = new CardType();
            var result = cardType.CardLogic(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
