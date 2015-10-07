namespace data_cleansing.net.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal sealed class Configuration : DbMigrationsConfiguration<data_cleansing.net.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(data_cleansing.net.Data.ApplicationDbContext context)
        {
           
        }
    }
}
