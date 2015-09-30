using data_cleansing.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Data
{
    public interface IUowData
    {
        IRepository<Credit> Credits { get; }

        IRepository<AddressCleansingHistory> AddressCleansingHistory { get; }

        IRepository<BicCodeEurope> BicCodeEurope { get; }

        IRepository<BicEurope> BicEurope { get; }

        IRepository<BillingInformation> BillingInformation { get; }

        IRepository<CardCleansingHistory> CardCleansingHistory { get; }

        IRepository<ClientAPICheck> ClientAPICheck { get; }

        IRepository<DailyTable> DailyTable { get; }

        IRepository<DeduplicateCleansingHistory> DeduplicateCleansingHistory { get; }

        IRepository<Digit> Digit { get; }

        IRepository<EmailCleansingHistory> EmailCleansingHistory { get; }

        IRepository<FullDetail> FullDetail { get; }

        IRepository<History> History { get; }

        IRepository<IbanCleansingHistory> IbanCleansingHistory { get; }

        IRepository<Invoice> Invoice { get; }

        IRepository<Messages> Messages { get; }

        IRepository<PhoneCleansingHistory> PhoneCleansingHistory { get; }

        IRepository<Profile> Profile { get; }

        IRepository<RawSortCode> RawSortCode { get; }

        IRepository<Subscribe> Subscribe { get; }

        IRepository<SwiftCode> SwiftCode { get; }

        IRepository<TemporaryUser> TemporaryUser { get; }

        IRepository<TestTable> TestTable { get; }

        IRepository<UKAreaCode> UKAreaCode { get; }

        IRepository<GetFullDetails> GetFullDetails { get; }

        IRepository<GetFullDetailsFromStreet> GetFullDetailsFromStreet { get; }

        IRepository<GetFullDetailsPartialStreet> GetFullDetailsPartialStreet { get; }

        IRepository<GetFullDetailsPlusStreet> GetFullDetailsPlusStreet { get; }

        IRepository<GetFullDetailsPlusStreetAndFlat> GetFullDetailsPlusStreetAndFlat { get; }
        
        int SaveChanges();
    }
}
