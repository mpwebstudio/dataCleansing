using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class Subscribe
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Email { get; set; }
    }
}
