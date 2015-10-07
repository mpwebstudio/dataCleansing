namespace data_validation.net.Web.Controllers.Validation.IbanValidation
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Mvc;

    using data_validation.net.Web.Controllers.Helpers;
    
    using data_validation.net.Web.ViewModels;
    public class IbanValidationController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult IbanValidation(string id, string id2)
        {
            string apiNumber = id2;

            if (User.Identity.IsAuthenticated)
            {
                var helper = new Helpers.CreditsDeduct();
                var res = helper.IsValid(User.Identity.Name, 6, id, "single", "iban");

                if (res == false)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(404, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var isValid = new IsValid();

                bool tUser = isValid.ValidUser(apiNumber, id, "iban");

                if (tUser == false)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(410, JsonRequestBehavior.AllowGet);
                }
            }

            var resT = new BankValidationController();

            var temp = BankValidationController.Iban.CheckIban(id, true);

            var error = new List<iBanBicModel>();

            if (temp.IsValid == false)
            {
                error.Add(new iBanBicModel { BankName = temp.Message, BranchName = id });

                return Json(error, JsonRequestBehavior.AllowGet);
            }

            var result = resT.BicCountry(id.ToUpper());

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}