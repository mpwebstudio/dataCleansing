namespace data_validation.net.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class GetSwift
    {
        [Range(8,11)]
        public string GetSwiftNumber { get; set; }
    }
}