using data_cleansing.net.Models;
using data_validation.net.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace data_validation.net.Web.Controllers
{
    public class ContactUsController : BaseController
    {
        public ActionResult Index()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Enquiry Type", Selected = true });

            items.Add(new SelectListItem { Text = "Address Validation", Value = "Address Validation" });

            items.Add(new SelectListItem { Text = "Address Cleansing", Value = "AddressCleansing" });

            items.Add(new SelectListItem { Text = "Email Validation", Value = "Email Validation" });

            items.Add(new SelectListItem { Text = "Email Cleansing", Value = "Email Cleansing" });

            items.Add(new SelectListItem { Text = "Debit/Credit card Validation", Value = "Debit/Credit card Validation" });

            items.Add(new SelectListItem { Text = "Debit/Credit card Cleansing", Value = "Debit/Credit card Cleansing" });

            items.Add(new SelectListItem { Text = "Swift Validation", Value = "Swift Validation" });

            items.Add(new SelectListItem { Text = "Swift Cleansing", Value = "Swift Cleansing" });

            items.Add(new SelectListItem { Text = "Iban Validation", Value = "Iban Validation" });

            items.Add(new SelectListItem { Text = "Iban Cleansing", Value = "Iban Cleansing" });

            items.Add(new SelectListItem { Text = "Deduplicate", Value = "Deduplicate" });

            items.Add(new SelectListItem { Text = "Other", Value = "Other" });

            ViewBag.EnquiryType = items;

            return View();
        }

        public ActionResult SendMessage(ContactUsModel contact)
        {
            this.Data.Messages.Add(new Messages
            {
                Context = contact.Context,
                EmailAddress = contact.Email,
                EnquiryType = contact.EnquiryType,
                Phone = contact.Phone,
                Subject = contact.Subject,
                Name = contact.Name
            });

            this.Data.SaveChanges();

            return View();
        }
    }
}