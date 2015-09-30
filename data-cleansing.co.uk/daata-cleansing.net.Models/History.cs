using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class History
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(25)]
        public string APINumber { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public string IP { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(75)]
        public string Host { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string Type { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string SearchKeyword { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(25)]
        public string Service { get; set; }
    }
}
