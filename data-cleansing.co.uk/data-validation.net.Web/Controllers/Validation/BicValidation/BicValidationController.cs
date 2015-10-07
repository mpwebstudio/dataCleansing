namespace data_validation.net.Web.Controllers.Validation.BicValidation
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using data_cleansing.net.Data;
    using data_validation.net.Web.Controllers.Helpers;
    using data_validation.net.Web.ViewModels;

    public class BicValidationController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult BicValidation(string id, string id2)
        {
            var apiNumber = id2;
            if (User.Identity.IsAuthenticated)
            {
                var helper = new Helpers.CreditsDeduct();
                var res = helper.IsValid(User.Identity.Name, 6, id, "single", "bic");

                if (res == false)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(404, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var isValid = new IsValid();

                bool tUser = isValid.ValidUser(apiNumber, id, "bic");

                if (tUser == false)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(410, JsonRequestBehavior.AllowGet);
                }
            }

            using (var context = new ApplicationDbContext())
            {
                var result = context.Database.SqlQuery<BicInfoModel>("GetFullDetailsSwift2 @SwiftCode", new SqlParameter("SwiftCode", id)).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}