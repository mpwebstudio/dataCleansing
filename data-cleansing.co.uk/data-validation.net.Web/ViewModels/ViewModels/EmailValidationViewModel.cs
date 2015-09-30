using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Models.ViewModels
{
    public class EmailValidationViewModel
    {
        public string Email { get; set; }

        public string IsValid { get; set; }

        public string MxRecord { get; set; }

        public string Message { get; set; }
    }
}