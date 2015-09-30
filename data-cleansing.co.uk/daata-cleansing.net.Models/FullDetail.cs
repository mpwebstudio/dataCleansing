using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class FullDetail
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(4)]
        public string BuildingNumber { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Flat { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(60)]
        public string BuildingName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Street { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(8)]
        public string PostCode { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(25)]
        public string Locality { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string TraditionalCounty { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string AdministrativeCounty { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(70)]
        public string OrganisationName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(40)]
        public string City { get; set; }
    }
}
