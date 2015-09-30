using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class RawSortCode
    {
        public int id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string SortCode { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        public string Address { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string BankName { get; set; }
    }
}
