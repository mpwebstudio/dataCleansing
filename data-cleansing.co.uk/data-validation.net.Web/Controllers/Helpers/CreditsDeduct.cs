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
     
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        
        public bool CheckForFraud(string userName, string ip, string host)
        {
            int result = 0;
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                SqlCommand cmd = new SqlCommand("CheckFraud", dbConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@return", System.Data.SqlDbType.Int);
                param.Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.Parameters.Add(param);
                cmd.Parameters.Add(new SqlParameter("@userName", userName));
                cmd.Parameters.Add(new SqlParameter("@ip", ip));
                cmd.Parameters.Add(new SqlParameter("@host", host));
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(param.Value);
            }

            if(result == 1)
            {
                return true;
            }

            return false;
        }
        
        
        public bool IsValid(string name, int credits, string searchKeyword, string type, string service)
        {
            int result = 0;
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                SqlCommand cmd = new SqlCommand("CheckWhenUserIsAuthenticated2", dbConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@return", System.Data.SqlDbType.Int);
                param.Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.Parameters.Add(param);
                cmd.Parameters.Add(new SqlParameter("@userName", name));
                cmd.Parameters.Add(new SqlParameter("@credits", credits));
                cmd.Parameters.Add(new SqlParameter("@type", type));
                cmd.Parameters.Add(new SqlParameter("@searchKeyword", searchKeyword));
                cmd.Parameters.Add(new SqlParameter("@service", service));
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(param.Value);
            }

            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public bool IsValidApiUser(string ip, string host, string apiNumber, int credits, string searchKeyword, string type, string service)
        {
            int result = 0;
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                SqlCommand cmd = new SqlCommand("CheckWhenAPICall2", dbConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@return", System.Data.SqlDbType.Int);
                param.Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.Parameters.Add(param);
                cmd.Parameters.Add(new SqlParameter("@ip", ip.TrimEnd()));
                cmd.Parameters.Add(new SqlParameter("@credits", credits));
                cmd.Parameters.Add(new SqlParameter("@apiNumber", apiNumber));
                cmd.Parameters.Add(new SqlParameter("@host", host));
                cmd.Parameters.Add(new SqlParameter("@type", type));
                cmd.Parameters.Add(new SqlParameter("@searchKeyword", searchKeyword));
                cmd.Parameters.Add(new SqlParameter("@service", service));
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(param.Value);
            }

            if(result == 1)
            {
                return true;
            }

            return false;
        }

        public bool CheckTemporaryUser(string ip, int num)
        {
            //var result = db.CheckTemporaryUser(ip, num);

            int result = 0;

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
                result = Convert.ToInt32(param.Value);
            }

            if (result == 1)
            {
                return true;
            }
            return false;
        }
    }
}