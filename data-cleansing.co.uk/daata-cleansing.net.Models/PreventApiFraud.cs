using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class PreventApiFraud
    {
        [Key]
        public int Id { get; set; }

        [StringLength(250)]
        [Column(TypeName = "VARCHAR")]
        public string HostName { get; set; }

        [StringLength(15)]
        [Column(TypeName = "CHAR")]
        public string Ip { get; set; }

        public DateTime Date { get; set; }

        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string UserName { get; set; }
    }
}
