﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Models
{
    public class BicInfoModel
    {

        public int Id { get; set; }
        public string IsValid { get; set; }

        public string SwiftCode { get; set; }

        public string BankOrInstitution { get; set; }

        public string Branch { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string ErrorCode { get; set; }
    }
}