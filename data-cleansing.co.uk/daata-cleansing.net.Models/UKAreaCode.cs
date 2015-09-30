using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class UKAreaCode
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(6)]
        public string Code { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string AreaCovered { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public string IsValid { get; set; }
    }
}
