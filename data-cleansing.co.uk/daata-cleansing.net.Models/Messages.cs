using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class Messages
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Subject { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(1000)]
        public string Context { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public string Phone { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string EnquiryType { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
