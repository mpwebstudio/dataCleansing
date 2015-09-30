using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Models
{
    public class CustomerUploadModel
    {
        public string[] fildsName { get; set; }

        public string csvFile { get; set; }
    }
}