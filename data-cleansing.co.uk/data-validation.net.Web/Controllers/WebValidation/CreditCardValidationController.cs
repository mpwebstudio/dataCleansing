using data_validation.net.Web.Controllers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace data_validation.net.Web.Controllers.WebValidation
{
    public class CreditCardValidationController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCard(string id, string id2)
        {
            var apiNumber = id2;

            if(User.Identity.IsAuthenticated)
            {
                var helper = new Helpers.CreditsDeduct();
                var res = helper.IsValid(User.Identity.Name, 6, id, "single", "email");

                if (res == false)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(404, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var isValid = new IsValid();

                bool tUser = isValid.ValidUser(apiNumber, id, "card");

                if (tUser == false)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(410, JsonRequestBehavior.AllowGet);
                }
            }

            var cardType = new CardType();
            var result = cardType.CardLogic(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}