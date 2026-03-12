using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using CentralData.Models;
using NGCP.BaseClass;


namespace CentralData.Class
{

	public class clsLogin
	{
        private IConfiguration? _configuration;
        public clsLogin(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string IsValidate(mLogin model)
        {
            //Open connection
            try
            {
                DBConnection conn = new DBConnection(_configuration);
                using (SqlConnection sqlConn = conn.AppConnection("LISCD"))
                {
                    //Create command and set connection for command
                    using (SqlCommand sqlCmd = sqlConn.CreateCommand())
                    {
                        //Define the commandtype and name with parameters
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "SP_UserLogin";
                        sqlCmd.Parameters.Add(new SqlParameter("@LoginName", SqlDbType.NVarChar)).Value = model.userName;
                        sqlCmd.Parameters.Add(new SqlParameter("@PassWord", SqlDbType.NVarChar)).Value = model.userPass;
                        sqlCmd.Parameters.Add(new SqlParameter("@AppCode", SqlDbType.NVarChar)).Value = model.appCode;
                        object returnValue = sqlCmd.ExecuteScalar();
                        return returnValue.ToString();
                    }
                }
            }
            catch (Exception errMsg)
            {

                return errMsg.ToString();
            }

        }

    }
}