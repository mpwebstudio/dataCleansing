using data_cleansing.net.Data;
using data_cleansing.net.Models;
using data_validation.net.Web.Controllers.Helpers;
using data_validation.net.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace data_validation.net.Web.Controllers.WebValidation
{
    public class TelephoneValidationController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult TelephoneValidation(string id, string id2)
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

            var remove = new RemoveSpecialCharacters();
                
                var number = remove.RemoveCharacters(id);

            var result = new List<PhoneValidationViewModel>();

            if (number.Length != 11 && number.Length != 10)
            {
                result.Add(new PhoneValidationViewModel
                {
                    IsValid = "Phone is invalid",
                    Number = id
                });

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            using (var context = new ApplicationDbContext())
            {
                for (int i = 5; i >= 2; i--)
                {
                    var numberPer = new SqlParameter("@code", number.Substring(0, i));

                    var tmpData = context.Database.SqlQuery<GetPhoneArea_Result>("GetPhoneArea2 @code", numberPer).First();

                    if (tmpData.IsValid == "Valid")
                    {
                        result.Add(new PhoneValidationViewModel
                        {
                            Area = tmpData.AreaCovered,
                            Number = id,
                            IsValid = tmpData.IsValid
                        });
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            result.Add(new PhoneValidationViewModel
            {

                IsValid = "Phone seems invalid"
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}