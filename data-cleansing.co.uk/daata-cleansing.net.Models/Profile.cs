using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class Profile
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(25)]
        public string FirstName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(25)]
        public string SurName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public string Telephone { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Email { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string WebPage { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string ApiNumber { get; set; }
        public Nullable<System.DateTime> DataRegister { get; set; }
    }
}
