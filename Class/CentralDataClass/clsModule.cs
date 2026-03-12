
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using NGCP.BaseModel;
using NGCP.BaseClass;
using CentralData.Models;

namespace CentralData.Class
{
    public class clsModule
    {
        private IConfiguration _configuration;
        public clsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable GET_DATA(mGenericParameter model)
        {
            try
            {
                DBConnection dBConnection = new DBConnection(_configuration);
                //Open connection
                using (SqlConnection sqlConn = dBConnection.AppConnection("LISCD"))
                {
                    //Create command and set connection for command
                    using (SqlCommand sqlCmd = sqlConn.CreateCommand())
                    {
                        //Define the commandtype and name with parameters
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "SP_Module_Qry";
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

        public string CUD(mModule model)
        {
            //Open connection
            DBConnection dBConnection = new DBConnection(_configuration);
            using (SqlConnection sqlConn = dBConnection.AppConnection("LISCD"))
            {
                //Create command and set connection for command
                using (SqlCommand sqlCmd = sqlConn.CreateCommand())
                {
                    //Define the commandtype and name with parameters
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "SP_Module";
                    sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = model._action;
                    sqlCmd.Parameters.Add(new SqlParameter("@Id ", SqlDbType.Int)).Value = model.id;
                    sqlCmd.Parameters.Add(new SqlParameter("@ModuleCode ", SqlDbType.NVarChar)).Value = model.ModuleCode;
                    sqlCmd.Parameters.Add(new SqlParameter("@ModuleDesc ", SqlDbType.NVarChar)).Value = model.ModuleDesc;
                    sqlCmd.Parameters.Add(new SqlParameter("@AppCode ", SqlDbType.NVarChar)).Value = model.AppCode;
                    sqlCmd.Parameters.Add(new SqlParameter("@ObjectName ", SqlDbType.NVarChar)).Value = model.ObjectName;
                    sqlCmd.Parameters.Add(new SqlParameter("@MCActive", SqlDbType.Bit)).Value = model.MCActive;
                    sqlCmd.Parameters.Add(new SqlParameter("@Remarks ", SqlDbType.NVarChar)).Value = model.remarks;
                    sqlCmd.Parameters.Add(new SqlParameter("@RecordStatus", SqlDbType.Bit)).Value = model.recordStatus;
                    sqlCmd.Parameters.Add(new SqlParameter("@UserCode", SqlDbType.NVarChar)).Value = model.userCode;
                    sqlCmd.Parameters.Add(new SqlParameter("@PCCode", SqlDbType.NVarChar)).Value = model.pcCode;

                    object returnValue = sqlCmd.ExecuteScalar();
                    return returnValue.ToString();
                }
            }
        }
    }
}