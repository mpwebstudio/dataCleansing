using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Models
{
    public class ContactUsModel
    {
        public string Subject { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Context { get; set; }

        public string Phone { get; set; }

        public string EnquiryType { get; set; }
    }
}