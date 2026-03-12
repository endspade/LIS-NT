using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Web;
using NGCP.BaseModel;
using NGCP.BaseClass;
using CentralData.Models;


namespace CentralData.Class
{
    public class clsRBACGroup
    {
        private IConfiguration? _configuration;
        public clsRBACGroup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable GET_DATA(mGenericParameter model)
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
                        sqlCmd.CommandText = "SP_RBACGroup_Qry";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = model._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@Intparam", SqlDbType.Int)).Value = model.intParam;
                        sqlCmd.Parameters.Add(new SqlParameter("@Strparam", SqlDbType.NVarChar)).Value = model.strParam;
                        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                        DataSet ds = new DataSet();
                        sqlDa.Fill(ds);
                        DataTable dtTable = ds.Tables[0];
                        return dtTable;
                    }
                }
            }
            catch (Exception errMessage)
            {
                errMessage.ToString();
                return null;
            }
        }

        public string CUD(mRBAC model)
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
                        sqlCmd.CommandText = "SP_RBACGroup";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = model._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@Id ", SqlDbType.Int)).Value = model.id;
                        sqlCmd.Parameters.Add(new SqlParameter("@AppCode ", SqlDbType.NVarChar)).Value = model.appCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@RBACCode ", SqlDbType.NVarChar)).Value = model.RBACCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@RBACDesc ", SqlDbType.NVarChar)).Value = model.RBACDesc;
                        sqlCmd.Parameters.Add(new SqlParameter("@RecordStatus", SqlDbType.Bit)).Value = model.recordStatus;
                        sqlCmd.Parameters.Add(new SqlParameter("@UserCode", SqlDbType.NVarChar)).Value = model.userCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@PCCode", SqlDbType.NVarChar)).Value = model.pcCode;
                        object returnValue = sqlCmd.ExecuteScalar();
                        return returnValue.ToString();
                    }
                }
            }
            catch (Exception e )
            {

                return e.ToString();
            }
           
        }
    }
}