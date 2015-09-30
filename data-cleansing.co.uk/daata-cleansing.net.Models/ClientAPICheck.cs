using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class ClientAPICheck
    {
        public int Id { get; set; }
        public Nullable<int> ClientId { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string ClientAPIkey { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string ClientIPAddress { get; set; }
        public Nullable<int> ClientUnitsRemain { get; set; }
    }
}
