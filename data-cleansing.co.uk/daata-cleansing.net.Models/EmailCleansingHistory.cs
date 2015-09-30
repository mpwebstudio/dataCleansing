using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class EmailCleansingHistory
    {
        public int Id { get; set; }
        public Nullable<int> ValidEmail { get; set; }
        public Nullable<int> InvalidEmail { get; set; }
        public Nullable<int> SyntaxValid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string UserName { get; set; }
        public Nullable<System.DateTime> DateSubmited { get; set; }
        public Nullable<int> RecordsUploaded { get; set; }
    }
}
