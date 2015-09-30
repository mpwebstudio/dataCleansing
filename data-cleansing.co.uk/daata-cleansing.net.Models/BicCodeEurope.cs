using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class BicCodeEurope
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string BankCode { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string BankOrInstitution { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Address { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string City { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Country { get; set; }
    }
}
