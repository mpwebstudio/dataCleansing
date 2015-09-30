using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class Credit
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string UserName { get; set; }
        public Nullable<int> Credits { get; set; }
        public Nullable<System.DateTime> DatePurchase { get; set; }
        public Nullable<System.DateTime> DateExpire { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string ApiNumber { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string Host { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public string IP { get; set; }
    }
}
