using data_cleansing.net.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Data
{
    public class UowData : IUowData
    {
        private readonly DbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();


        public UowData()
            : this(new ApplicationDbContext())
        {
        }

        public UowData(DbContext context)
        {
            this.context = context;
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public IRepository<Credit> Credits
        {
            get { return this.GetRepository<Credit>(); }
        }

        public IRepository<AddressCleansingHistory> AddressCleansingHistory
        {
            get { return this.GetRepository<AddressCleansingHistory>(); }
        }

        public IRepository<BicCodeEurope> BicCodeEurope
        {
            get { return this.GetRepository<BicCodeEurope>(); }
        }

        public IRepository<BicEurope> BicEurope
        {
            get { return this.GetRepository<BicEurope>(); }
        }

        public IRepository<BillingInformation> BillingInformation
        {
            get { return this.GetRepository<BillingInformation>(); }
        }

        public IRepository<CardCleansingHistory> CardCleansingHistory
        {
            get { return this.GetRepository<CardCleansingHistory>(); }
        }

        public IRepository<ClientAPICheck> ClientAPICheck
        {
            get { return this.GetRepository<ClientAPICheck>(); }
        }

        public IRepository<DailyTable> DailyTable
        {
            get { return this.GetRepository<DailyTable>(); }
        }

        public IRepository<DeduplicateCleansingHistory> DeduplicateCleansingHistory
        {
            get { return this.GetRepository<DeduplicateCleansingHistory>(); }
        }


        public IRepository<Digit> Digit
        {
            get { return this.GetRepository<Digit>(); }
        }

        public IRepository<EmailCleansingHistory> EmailCleansingHistory
        {
            get { return this.GetRepository<EmailCleansingHistory>(); }
        }

        public IRepository<FullDetail> FullDetail
        {
            get { return this.GetRepository<FullDetail>(); }
        }

        public IRepository<History> History
        {
            get { return this.GetRepository<History>(); }
        }

        public IRepository<IbanCleansingHistory> IbanCleansingHistory
        {
            get { return this.GetRepository<IbanCleansingHistory>(); }
        }

        public IRepository<Invoice> Invoice
        {
            get { return this.GetRepository<Invoice>(); }
        }

        public IRepository<Messages> Messages
        {
            get { return this.GetRepository<Messages>(); }
        }

        public IRepository<PhoneCleansingHistory> PhoneCleansingHistory
        {
            get { return this.GetRepository<PhoneCleansingHistory>(); }
        }

        public IRepository<PreventApiFraud> PreventApiFraud
        {
            get { return this.GetRepository<PreventApiFraud>(); }
        }

        public IRepository<Profile> Profile
        {
            get { return this.GetRepository<Profile>(); }
        }

        public IRepository<RawSortCode> RawSortCode
        {
            get { return this.GetRepository<RawSortCode>(); }
        }

        public IRepository<Subscribe> Subscribe
        {
            get { return this.GetRepository<Subscribe>(); }
        }

        public IRepository<SwiftCode> SwiftCode
        {
            get { return this.GetRepository<SwiftCode>(); }
        }

        public IRepository<TemporaryUser> TemporaryUser
        {
            get { return this.GetRepository<TemporaryUser>(); }
        }

        public IRepository<TestTable> TestTable
        {
            get { return this.GetRepository<TestTable>(); }
        }


        public IRepository<UKAreaCode> UKAreaCode
        {
            get { return this.GetRepository<UKAreaCode>(); }
        }

        public IRepository<UKSortCode> UKSortCode
        {
            get { return this.GetRepository<UKSortCode>(); }
        }

        public IRepository<GetFullDetails> GetFullDetails
        {
            get { return this.GetRepository<GetFullDetails>(); }
        }

        public IRepository<GetFullDetailsFromStreet> GetFullDetailsFromStreet
        {
            get { return this.GetRepository<GetFullDetailsFromStreet>(); }
        }

        public IRepository<GetFullDetailsPartialStreet> GetFullDetailsPartialStreet
        {
            get { return this.GetRepository<GetFullDetailsPartialStreet>(); }
        }

        public IRepository<GetFullDetailsPlusStreet> GetFullDetailsPlusStreet
        {
            get { return this.GetRepository<GetFullDetailsPlusStreet>(); }
        }

        public IRepository<GetFullDetailsPlusStreetAndFlat> GetFullDetailsPlusStreetAndFlat
        {
            get { return this.GetRepository<GetFullDetailsPlusStreetAndFlat>(); }
        }


    }
}
