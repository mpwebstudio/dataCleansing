using data_validation.net.Web.Controllers.Helpers;
using data_validation.net.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace data_validation.net.Web.Controllers.WebValidation
{
    public class AddressValidationController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
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
                    error.Add(new ResultForPostCode { PostCode = "You do not have enough credits remain" });
                    return Json(error, JsonRequestBehavior.AllowGet);
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
            var dataCleansing = new DataCleansing.DataCleansingLogich();
            var temp = dataCleansing.Cleansing(id);
            return Json(temp, JsonRequestBehavior.AllowGet);
        }
    }
}