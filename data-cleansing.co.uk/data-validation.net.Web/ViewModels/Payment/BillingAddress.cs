using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Models.Payment

{
    public class BillingAddress
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string CompanyName { get; set; }

        public string CompanyNumber { get; set; }

        public string CountryCode { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string Address1 { get; set; }

        public string NormalizationStatus { get; set; }

        public string PostCode { get; set; }

        public string State { get; set; }

        public string Status { get; set; }

        public string Phone { get; set; }

        public string VatNumber { get; set; }

        public string Www { get; set; }
    }
}