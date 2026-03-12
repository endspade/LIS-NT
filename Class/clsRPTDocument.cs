using Microsoft.Data.SqlClient;
using NGCP.BaseClass;
using NGCP.BaseModel;
using NGCP.LIS_NT.Models;
using System.Data;
using System.Reflection.Emit;

namespace NGCP.LIS_NT.Class
{
    public class clsRPTDocument
    {
        private IConfiguration? _configuration;
        public clsRPTDocument(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable GET_DATA(mGenericParameter model)
        {
            try
            {
                //Open connection
                DBConnection conn = new DBConnection(_configuration);
                using (SqlConnection sqlConn = conn.AppConnection("LISNT"))
                {
                    //Create command and set connection for command
                    using (SqlCommand sqlCmd = sqlConn.CreateCommand())
                    {
                        //Define the commandtype and name with parameters
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "SP_RPTDocument_Qry";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = model._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@Strparam", SqlDbType.NVarChar)).Value = model.strParam;
                        sqlCmd.Parameters.Add(new SqlParameter("@Intparam", SqlDbType.Int)).Value = model.intParam;
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

        public string CUD(mRPTDocument param)
        {
            //Open connection
            try
            {
                DBConnection conn = new DBConnection(_configuration);
                using (SqlConnection sqlConn = conn.AppConnection("LISNT"))
                {
                    //Create command and set connection for command
                    using (SqlCommand sqlCmd = sqlConn.CreateCommand())
                    {
                        //Define the commandtype and name with parameters
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "SP_RPTDocument";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = param._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@Id ", SqlDbType.Int)).Value = param.id;
                        sqlCmd.Parameters.Add(new SqlParameter("@DocIn", SqlDbType.Bit)).Value = param.docIn;
                        sqlCmd.Parameters.Add(new SqlParameter("@DocGuid", SqlDbType.NVarChar)).Value = param.docGuid;
                        sqlCmd.Parameters.Add(new SqlParameter("@PDCode", SqlDbType.NVarChar)).Value = param.pdCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@DocumentDesc", SqlDbType.NVarChar)).Value = param.documentDesc;
                        sqlCmd.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.NVarChar)).Value = param.remarks;
                        sqlCmd.Parameters.Add(new SqlParameter("@RecordStatus", SqlDbType.Bit)).Value = param.recordStatus;
                        sqlCmd.Parameters.Add(new SqlParameter("@UserCode", SqlDbType.NVarChar)).Value = param.userCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@PCCode", SqlDbType.NVarChar)).Value = param.pcCode;
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
    }
}
