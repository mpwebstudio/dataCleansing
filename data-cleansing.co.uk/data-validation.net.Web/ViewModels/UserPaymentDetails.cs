using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Models
{
    public class UserPaymentDetails
    {
        public int InvoiceNo { get; set; }

        public string Amount { get; set; }
    }
}