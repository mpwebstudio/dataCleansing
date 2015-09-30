using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class GetBicDetails_Result
    {
        public string BankOrInstitution { get; set; }
        public string City { get; set; }
        public string Branch { get; set; }
        public string SwiftCode { get; set; }
        public string Country { get; set; }
        public Nullable<int> CountryId { get; set; }
    }
}
