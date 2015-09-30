using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Models
{
    public class IbanBankCode
    {
        public int Id { get; set; }

        public string BankCode { get; set; }

        public string BankName { get; set; }

        public string Address { get; set; }
        
        public string City { get; set; }

        public string  Country { get; set; }
    }
}