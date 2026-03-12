using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Web;
using NGCP.BaseModel;
using NGCP.BaseClass;

namespace CentralData.Class
{
    public class clsAutoNumber
    {
        private IConfiguration _configuration;
        public clsAutoNumber(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public  string GET_Number(mGenericParameter model)
        {
            try
            {
                //Open connection
                DBConnection conn = new DBConnection(_configuration);
                using (SqlConnection sqlConn = conn.AppConnection("LISCD"))
                {
                    //Create command and set connection for command
                    using (SqlCommand sqlCmd = sqlConn.CreateCommand())
                    {
                        //Define the commandtype and name with parameters
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "SP_AutoNumbering_Qry";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = model._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@StrParam", SqlDbType.NVarChar)).Value = model.strParam;
                        sqlCmd.Parameters.Add(new SqlParameter("@IntParam", SqlDbType.Int)).Value = model.intParam;
                        sqlCmd.Parameters.Add(new SqlParameter("@NewNumber", SqlDbType.NVarChar, 255)).Value = ParameterDirection.Output;
                        object returnValue = sqlCmd.ExecuteScalar();
                        return returnValue.ToString();
                    }
                }
            }
            catch (Exception errMessage)
            {
                //errMessage.ToString();
                return errMessage.ToString(); 
            }
        }

    }
}