﻿namespace data_validation.net.Web.Models.ViewModels
{
    public class PhoneValidationViewModel
    {
        public string Number { get; set; }

        public string IsValid { get; set; }

        public string Area { get; set; }

        public string Message { get; set; }
    }
}