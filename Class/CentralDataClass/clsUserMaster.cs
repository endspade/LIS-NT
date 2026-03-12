using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Web;
using CentralData.Models;
using NGCP.BaseClass;
using NGCP.BaseModel;



namespace CentralData.Class
{
    public class clsUserMaster
    {
        private IConfiguration? _configuration;
        public clsUserMaster(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public  string CUD(mUserMaster model)
        {
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
                        sqlCmd.CommandText = "SP_UserMaster";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = model._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = model.id;
                        sqlCmd.Parameters.Add(new SqlParameter("@UserNumber", SqlDbType.NVarChar)).Value = model.userNumber;
                        sqlCmd.Parameters.Add(new SqlParameter("@LoginName", SqlDbType.NVarChar)).Value = model.loginName;
                        sqlCmd.Parameters.Add(new SqlParameter("@PassWord", SqlDbType.NVarChar)).Value = model.passWord;
                        sqlCmd.Parameters.Add(new SqlParameter("@AppCode", SqlDbType.NVarChar)).Value = model.appCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@LastLogin", SqlDbType.NVarChar)).Value = model.lastLogin;
                        sqlCmd.Parameters.Add(new SqlParameter("@EmployeeTypeID", SqlDbType.NVarChar)).Value = model.employeeTypeId;
                        sqlCmd.Parameters.Add(new SqlParameter("@EmployeeNumber", SqlDbType.NVarChar)).Value = model.employeeNumber;
                        sqlCmd.Parameters.Add(new SqlParameter("@EmployeeName", SqlDbType.NVarChar)).Value = model.employeeName;
                        sqlCmd.Parameters.Add(new SqlParameter("@UserPicture", SqlDbType.VarBinary)).Value = new byte[0];
                        sqlCmd.Parameters.Add(new SqlParameter("@DOB", SqlDbType.DateTime)).Value = model.dob;
                        sqlCmd.Parameters.Add(new SqlParameter("@DateFr", SqlDbType.DateTime)).Value = model.dateFr;
                        sqlCmd.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.DateTime)).Value = model.dateTo;
                        sqlCmd.Parameters.Add(new SqlParameter("@LockoutEnd", SqlDbType.DateTime)).Value = model.lockoutEnd;
                        sqlCmd.Parameters.Add(new SqlParameter("@LoginAttempts", SqlDbType.Int)).Value = model.loginAttempt;
                        sqlCmd.Parameters.Add(new SqlParameter("@AccessActive", SqlDbType.Bit)).Value = model.accessActive;
                        sqlCmd.Parameters.Add(new SqlParameter("@ActiveDirectory", SqlDbType.Bit)).Value = model.activeDirectory;
                        sqlCmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit)).Value = model.recordStatus;
                        sqlCmd.Parameters.Add(new SqlParameter("@USERCODE", SqlDbType.NVarChar)).Value = model.userCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@PCCODE", SqlDbType.NVarChar)).Value = model.pcCode;
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

        public  DataTable GET_DATA(mGenericParameter model)
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
                        sqlCmd.CommandText = "SP_UserMaster_Qry";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = model._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@AppCode", SqlDbType.NVarChar)).Value = model.appCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@LoginName", SqlDbType.NVarChar)).Value = model.strParam;
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



        public string GET_USERNUMBER(mGenericParameter model)
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
                        sqlCmd.CommandText = "SP_UserMaster_Qry";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = model._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@AppCode", SqlDbType.NVarChar)).Value = model.appCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@LoginName", SqlDbType.NVarChar)).Value = model.strParam;
                        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                        DataSet ds = new DataSet();
                        sqlDa.Fill(ds);
                        DataTable dtTable = ds.Tables[0];
                        return dtTable.Rows[0]["UserNumber"].ToString();
                    }
                }

            }
            catch (Exception errMessage)
            {
                errMessage.ToString();
                return null;
            }
        }
    }
}