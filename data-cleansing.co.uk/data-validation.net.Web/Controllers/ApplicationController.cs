using data_cleansing.net.Data;
using data_cleansing.net.Models;
using data_validation.net.Web.ViewModels.Spa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace data_validation.net.Web.Controllers
{
    [Authorize]
    public class ApplicationController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.Email = this.Data.Profile.All().Where(x => x.UserName == User.Identity.Name).Select(x => x.Email).First();
            return View();
        }

        public ActionResult SuccessView()
        {
            return View();
        }

        public JsonResult UserData()
        {
            var id = this.Data.Profile.All().Where(x => x.UserName == User.Identity.Name).First();
            var profileData = db.Profile.Find(id.Id);

            return Json(profileData, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult SaveInfo(Profile MyEvent)
        {
            db.Entry(MyEvent).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();

            var newUserInfo = this.Data.Profile.All().Where(x => x.UserName == User.Identity.Name).First();

            return Json(newUserInfo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCredits()
        {
            var credits = this.Data.Credits.All().Where(x => x.UserName == User.Identity.Name).Select(x => new GetCreditsModel { Credits = x.Credits, DateExpire = x.DateExpire }).First();

            return Json(credits, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BillingInfo()
        {
            var id = this.Data.Profile.All().Where(x=> x.UserName == User.Identity.Name).First();

            var billingInfo = db.BillingInformation.Find(id.Id);
            
            return Json(billingInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveBilling(BillingInformation MyEvent)
        {
            db.Entry(MyEvent).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();

            return Json(MyEvent, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetInvoice()
        {
            var invoice = this.Data.Invoice.All().Where(x => x.UserName == User.Identity.Name);

            return Json(invoice, JsonRequestBehavior.AllowGet);
        }
    }
}