using data_cleansing.net.Models;
using data_cleansing.net.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace data_validation.net.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }

        public JsonResult Subscribe(string id)
        {

            bool isEmail = Regex.IsMatch(id, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (isEmail)
            {

                var test = this.Data.Subscribe.All().FirstOrDefault(x => x.Email == id);

                if (test != null)
                {
                    return Json("exist", JsonRequestBehavior.AllowGet);
                }

                this.Data.Subscribe.Add(new data_cleansing.net.Models.Subscribe
                {
                    Email = id
                });

                this.Data.SaveChanges();

                return Json("good", JsonRequestBehavior.AllowGet);
            }

            return Json("bad", JsonRequestBehavior.AllowGet);
        }

        //Old

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TermsofUse()
        {
            return View();
        }
    }
}