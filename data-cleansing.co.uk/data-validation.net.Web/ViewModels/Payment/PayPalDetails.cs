using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Models.Payment
{
    public class PayPalDetails
    {
        public int Id { get; set; }

        public int AddressId { get; set; }

        public string ItemName { get; set; }

        public string ItemPrice { get; set; }

        public string InvoiceNo { get; set; }
    }
}