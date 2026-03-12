
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
    public class clsSFInfo
    {
        private IConfiguration? _configuration;
        public clsSFInfo(IConfiguration configuration)
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
                        sqlCmd.CommandText = "SP_SFInfo_Qry";
                        sqlCmd.Parameters.Add(new SqlParameter("@AppCode", SqlDbType.NVarChar)).Value = model.appCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@RBACCode", SqlDbType.NVarChar)).Value = model.rbacCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@ModuleCode", SqlDbType.NVarChar)).Value = model.moduleCode;
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

        public  string CUD(mSFInfo model)
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
                        sqlCmd.CommandText = "SP_SFInfo";
                        //sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = model._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@Id ", SqlDbType.Int)).Value = model.id;
                        sqlCmd.Parameters.Add(new SqlParameter("@AppCode ", SqlDbType.NVarChar)).Value = model.appCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@ModuleCode ", SqlDbType.NVarChar)).Value = model.moduleCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@RBACCode ", SqlDbType.NVarChar)).Value = model.rbacCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@SFInfo ", SqlDbType.NVarChar)).Value = model.sfInfo;
                        sqlCmd.Parameters.Add(new SqlParameter("@RecordStatus", SqlDbType.Bit)).Value = model.recordStatus;
                        sqlCmd.Parameters.Add(new SqlParameter("@UserCode", SqlDbType.NVarChar)).Value = model.userCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@PCCode", SqlDbType.NVarChar)).Value = model.pcCode;

                        object returnValue = sqlCmd.ExecuteScalar();
                        return returnValue.ToString();
                    }
                }
            }
            catch (Exception e)
            {

                return e.ToString();
            }

           
        }

        public bool ModuleAccess(mGenericParameter param)
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
                        sqlCmd.CommandText = "SP_AccessInfo_Qry";
                        sqlCmd.Parameters.Add(new SqlParameter("@AppCode", SqlDbType.NVarChar)).Value = param.appCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@ModuleCode", SqlDbType.NVarChar)).Value = param.moduleCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@UserNumber", SqlDbType.NVarChar)).Value = param.strParam;
                        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                        DataSet ds = new DataSet();
                        sqlDa.Fill(ds);
                        DataTable dtTable = ds.Tables[0];

                        int v_access = Convert.ToInt32(dtTable.Rows[0]["SFINFO"].ToString().Substring(0, 1));
                        if (v_access != 0)
                        {
                            return true;
                        }

                        return false;

                    }
                }
            }
            catch (Exception errMessage)
            {
                errMessage.ToString();
                return false;
            }
        }
        public string AccessInfo(mGenericParameter param)
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
                        sqlCmd.CommandText = "SP_AccessInfo_Qry";
                        sqlCmd.Parameters.Add(new SqlParameter("@AppCode", SqlDbType.NVarChar)).Value = param.appCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@ModuleCode", SqlDbType.NVarChar)).Value = param.moduleCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@UserNumber", SqlDbType.NVarChar)).Value = param.strParam;
                        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                        DataSet ds = new DataSet();
                        sqlDa.Fill(ds);
                        DataTable dtTable = ds.Tables[0];

                        return dtTable.Rows[0]["SFINFO"].ToString();

                    }
                }
            }
            catch (Exception errMessage)
            {
                //errMessage.ToString();
                return errMessage.ToString(); ;
            }
        }
    }
}