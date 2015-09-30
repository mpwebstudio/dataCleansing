using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class GetFullDetailsFromStreet
    {
        public int Id { get; set; }
        public string BuildingNumber { get; set; }
        public string Flat { get; set; }
        public string BuildingName { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string Locality { get; set; }
        public string TraditionalCounty { get; set; }
        public string AdministrativeCounty { get; set; }
        public string OrganisationName { get; set; }
        public string City { get; set; }
    }
}
