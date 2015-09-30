using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
   public class GetFullDetailsBankBranch_Result
    {
        public double Id { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Country { get; set; }
        public string IsoCode { get; set; }
        public string Swift { get; set; }
        public string Postcode { get; set; }
    }
}
