namespace data_validation.net.Web.Controllers.Validation.EmailValidation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using data_validation.net.Web.Controllers.Helpers;
    using data_validation.net.Web.Models.ViewModels;

    public class EmailValidationController :BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult EmailValidation(string id, string id2)
        {
            var apiNumber = id2;

            if (User.Identity.IsAuthenticated)
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

                bool tUser = isValid.ValidUser(apiNumber, id, "email");

                if (tUser == false)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(410, JsonRequestBehavior.AllowGet);
                }
            }

            var tmpResilts = new Helpers.Email();
            var tmpResult = tmpResilts.Validate(id);

            return Json(tmpResult, JsonRequestBehavior.AllowGet);
        }
    }
}