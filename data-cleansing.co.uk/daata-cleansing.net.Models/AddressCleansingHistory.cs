using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class AddressCleansingHistory
    {
        public int Id { get; set; }

        public int RecordsUploaded { get; set; }

        public int AddressCorrected { get; set; }

        public int AddressNotFound { get; set; }

        public DateTime DateSubmited { get; set; }
        
        [StringLength(50)]
        public string UserName { get; set; }
    }
}
