using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Models
{
    public class GetIban
    {
        [Range(15,32)]
        public string GetIbanNumber { get; set; }
    }
}