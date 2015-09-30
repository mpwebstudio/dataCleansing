using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class SwiftCode
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string BankOrInstitution { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string City { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string Branch { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public string SwiftCode1 { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Country { get; set; }
        public Nullable<int> CountryId { get; set; }
    }
}
