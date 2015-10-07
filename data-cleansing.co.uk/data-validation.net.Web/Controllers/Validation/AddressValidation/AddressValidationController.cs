namespace data_validation.net.Web.Controllers.Validation.AddressValidation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using data_validation.net.Web.Controllers.Helpers;
    using data_validation.net.Web.ViewModels;

    public class AddressValidationController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAddress(string id, string id2)
        {
            var apiNumber = id2;

            var error = new List<ResultForPostCode>();

            if (User.Identity.IsAuthenticated)
            {
                var helper = new Helpers.CreditsDeduct();
                var res = helper.IsValid(User.Identity.Name, 6, id, "single", "address");

                if (res == false)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(404, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var isValid = new IsValid();

                bool tUser = isValid.ValidUser(apiNumber, id, "address");

                if (tUser == false)
                {
                    error.Add(new ResultForPostCode { PostCode = "You have reach your free daily limits!" });
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
            }
            var dataCleansing = new DataCleansingLogich();
            var temp = dataCleansing.Cleansing(id);
            return Json(temp, JsonRequestBehavior.AllowGet);
        }
    }
}