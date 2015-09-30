using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Models.Spa
{
    public class EditUserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string Telephone { get; set; }

        public string WebPage { get; set; }
    }
}