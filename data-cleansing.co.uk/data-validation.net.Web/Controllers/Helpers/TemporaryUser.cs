using data_validation.net.Web.ViewModels.DataCleansing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace data_validation.net.Web.Controllers.Helpers
{
    public class TemporaryUser
    {
        
        public static bool IsValid(string ip, int num)
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
                return true;

            return false;
        }

        
    }
}