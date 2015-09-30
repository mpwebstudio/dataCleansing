using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Models
{
    public class UserDetailsModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public System.DateTime DateRegister { get; set; }
        public string ApiNumber { get; set; }
        public Nullable<int> Credits { get; set; }
    }
}