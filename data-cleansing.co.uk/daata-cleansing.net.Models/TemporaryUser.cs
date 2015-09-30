using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class TemporaryUser
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public string ip { get; set; }
        public Nullable<short> credits { get; set; }
    }
}
