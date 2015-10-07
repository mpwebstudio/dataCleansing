using data_cleansing.net.Data;
using data_validation.net.Web.ViewModels.DataCleansing;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Controllers.Helpers
{
    public class StoreProcedure
    {
        public List<AddressModel> FullDetailsPlusStreet(string post, string numb)
        {
            using (var context = new ApplicationDbContext())
            {
                var postPar = new SqlParameter("@PostCode", post);
                var numbPar = new SqlParameter("@Number", numb);

                var result = context.Database.SqlQuery<AddressModel>("GetFullDetailsPlusStreet2 @PostCode,@Number", postPar, numbPar).Select(x => new AddressModel
               {
                   Id = x.Id,
                   AdministrativeCounty = x.AdministrativeCounty,
                   BuildingName = x.BuildingName,
                   BuildingNumber = numb,
                   City = x.City,
                   Flat = x.Flat,
                   Locality = x.Locality,
                   PostCode = x.PostCode,
                   Street = x.Street,
                   TraditionalCounty = x.TraditionalCounty,
                   OrganisationName = x.OrganisationName,
                   IsValid = "Corrected"
               }).ToList();

                return result;
            }
        }

        public List<AddressModel> FullDetails(string post)
        {
            using (var context = new ApplicationDbContext())
            {
                var postPar = new SqlParameter("@PostCode", post);

                var result = context.Database.SqlQuery<AddressModel>("GetFullDetails2 @PostCode", postPar).Select(x => new AddressModel
                {
                    Id = x.Id,
                    AdministrativeCounty = x.AdministrativeCounty,
                    BuildingName = x.BuildingName,
                    BuildingNumber = x.BuildingNumber,
                    City = x.City,
                    Flat = x.Flat,
                    Locality = x.Locality,
                    PostCode = x.PostCode,
                    Street = x.Street,
                    TraditionalCounty = x.TraditionalCounty,
                    OrganisationName = x.OrganisationName,
                    IsValid = "Corrected"
                }).ToList();

                return result;

            }
        }

        public List<AddressModel> FullDetailsPlusFlat(string post, string numb, string flat)
        {
            using (var context = new ApplicationDbContext())
            {
                var postPar = new SqlParameter("@PostCode", post);
                var numbPar = new SqlParameter("@Number", numb);
                var flatPar = new SqlParameter("@Flat", flat);

                var result = context.Database.SqlQuery<AddressModel>("GetFullDetailsPlusStreetAndFlat2 @PostCode,@Number,@Flat", postPar, numbPar, flatPar).Select(x => new AddressModel
                {
                    Id = x.Id,
                    AdministrativeCounty = x.AdministrativeCounty,
                    BuildingName = x.BuildingName,
                    BuildingNumber = numb,
                    City = x.City,
                    Flat = x.Flat,
                    Locality = x.Locality,
                    PostCode = x.PostCode,
                    Street = x.Street,
                    TraditionalCounty = x.TraditionalCounty,
                    OrganisationName = x.OrganisationName,
                    IsValid = "Corrected"
                }).ToList();

                
                return result;
                
            }
        }

        public AddressModel FullDetailsFirstOrDefault(string post)
        {
            using (var context = new ApplicationDbContext())
            {
                var postPar = new SqlParameter("@PostCode", post);

                var result = context.Database.SqlQuery<AddressModel>("GetFullDetailsPlusStreet2 @PostCode", postPar).Select(x => new AddressModel
                {
                    Id = x.Id,
                    AdministrativeCounty = x.AdministrativeCounty,
                    BuildingName = x.BuildingName,
                    BuildingNumber = x.BuildingNumber,
                    City = x.City,
                    Flat = x.Flat,
                    Locality = x.Locality,
                    PostCode = x.PostCode,
                    Street = x.Street,
                    TraditionalCounty = x.TraditionalCounty,
                    OrganisationName = x.OrganisationName,
                    IsValid = "Corrected"
                }).FirstOrDefault();

                return result;

            }
        }

        public List<AddressModel> FullDetailsPartialStreet(string post, string street)
        {
            using (var context = new ApplicationDbContext())
            {
                var postPar = new SqlParameter("@PostCode", post);
                var streetPar = new SqlParameter("@Street", street);

                var result = context.Database.SqlQuery<AddressModel>("GetFullDetailsPartialStreet2 @PostCode,@Street", postPar, streetPar).Select(x => new AddressModel
                {
                    Id = x.Id,
                    AdministrativeCounty = x.AdministrativeCounty,
                    BuildingName = x.BuildingName,
                    BuildingNumber = x.BuildingNumber,
                    City = x.City,
                    Flat = x.Flat,
                    Locality = x.Locality,
                    PostCode = x.PostCode,
                    Street = x.Street,
                    TraditionalCounty = x.TraditionalCounty,
                    OrganisationName = x.OrganisationName,
                    IsValid = "Corrected"
                }).ToList();

                return result;
            }
        }
    }
}