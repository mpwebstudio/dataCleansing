namespace data_validation.net.Web.Controllers.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;

    using data_cleansing.net.Data;
    using data_cleansing.net.Models;
    using daata_cleansing.net.Models;

    public class CreditsDeduct
    {
        public bool IsValid(string name, int credits, string id, string type, string service)
        {
            int res = 0;

            using (var context = new ApplicationDbContext())
            {
                var namePar = new SqlParameter("@userName", name);
                var creditsPar = new SqlParameter("@credits", credits);
                var typePar = new SqlParameter("@type", type);
                var serchKey = new SqlParameter("@searchKeyword", id);
                var servicePar = new SqlParameter("@service", service);

                var tRes = context.Database.SqlQuery<CheckWhenUserIsAuthenticated>("CheckWhenUserIsAuthenticated2 @userName, @credits, @type, @searchKeyword, @service", namePar, creditsPar, typePar, serchKey, servicePar);

                foreach (var item in tRes)
                {
                    res = item.ReturnValue;
                    break;
                }
            }

            if (res == 1)
            {
                return true;
            }

            return true;
        }

        public bool IsValidApiUser(string ip, string host, string apiNumber, int num, string id, string type, string service)
        {
            
            int res = 0;

            using (var context = new ApplicationDbContext())
            {
                var namePar = new SqlParameter("@ip", ip.TrimEnd());
                var creditsPar = new SqlParameter("@credits", num);
                var apinumberPar = new SqlParameter("@apiNumber", apiNumber);
                var hostPar = new SqlParameter("@host", host);
                var typePar = new SqlParameter("@type", type);
                var serchKey = new SqlParameter("@searchKeyword", id);
                var servicePar = new SqlParameter("@service", service);

                var tRes = context.Database.SqlQuery<CheckWhenAPICall>("CheckWhenAPICall2 @apiNumber,@ip,@host, @credits, @type, @searchKeyword, @service", apinumberPar,hostPar, namePar, creditsPar, typePar, serchKey, servicePar);

                foreach (var item in tRes)
                {
                    res = item.ReturnValue;
                    break;
                }
            }

            if (res == 1)
            {
                return true;
            }

            return false;
        }

        public bool CheckTemporaryUser(string ip, int num)
        {
            int res = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                SqlCommand cmd = new SqlCommand("CheckTemporaryUser2", dbConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@return", System.Data.SqlDbType.Int);
                param.Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.Parameters.Add(param);
                cmd.Parameters.Add(new SqlParameter("@ip", ip.TrimEnd()));
                cmd.Parameters.Add(new SqlParameter("@num", num));
                cmd.ExecuteNonQuery();
                res = Convert.ToInt32(param.Value);
            }

            if (res == 1)
            {
                return true;
            }
            return false;
        }
    }
}