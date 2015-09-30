using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Models.ViewModels
{
    public class CardValidationViewModel
    {
        public int Id { get; set; }

        public string IsValid { get; set; }

        public string CardIssue { get; set; }

        public string CardNumber { get; set; }

        public string Message { get; set; }
    }
}