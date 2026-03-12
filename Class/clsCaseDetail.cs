using Microsoft.Data.SqlClient;
using NGCP.BaseClass;
using NGCP.BaseModel;
using NGCP.LIS_NT.Models;
using System.Data;

namespace NGCP.LIS_NT.Class
{
    public class clsCaseDetail
    {
        private IConfiguration? _configuration;
        public clsCaseDetail(IConfiguration configuration)
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
                        sqlCmd.CommandText = "SP_CaseDetail_Qry";
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

        public string CUD(mCase param)
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
                        sqlCmd.CommandText = "SP_CaseDetail";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = param._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@Id ", SqlDbType.Int)).Value = param.id;
                        sqlCmd.Parameters.Add(new SqlParameter("@DocGuid", SqlDbType.NVarChar)).Value = param.docGuid;
                        sqlCmd.Parameters.Add(new SqlParameter("@DocNumber", SqlDbType.NVarChar)).Value = param.docNumber;
                        sqlCmd.Parameters.Add(new SqlParameter("@RefNumber", SqlDbType.NVarChar)).Value = param.refNumber;
                        sqlCmd.Parameters.Add(new SqlParameter("@IncomingNo", SqlDbType.NVarChar)).Value = param.incomingNo;
                        sqlCmd.Parameters.Add(new SqlParameter("@OutgoingNo", SqlDbType.NVarChar)).Value = param.outgoingNo;

                        sqlCmd.Parameters.Add(new SqlParameter("@DocCategory", SqlDbType.NVarChar)).Value = param.docCatCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@DocumentDesc", SqlDbType.NVarChar)).Value = param.docCatDesc;
                        sqlCmd.Parameters.Add(new SqlParameter("@Petitioner", SqlDbType.NVarChar)).Value = param.petitioner;
                        sqlCmd.Parameters.Add(new SqlParameter("@Judge", SqlDbType.NVarChar)).Value = param.judge;
                        sqlCmd.Parameters.Add(new SqlParameter("@DateReceived", SqlDbType.DateTime)).Value = param.dateReceived;
                        sqlCmd.Parameters.Add(new SqlParameter("@DateNxtHiring", SqlDbType.DateTime)).Value = param.dateNxtHiring;
                        sqlCmd.Parameters.Add(new SqlParameter("@PendingIncident", SqlDbType.NVarChar)).Value = param.pendingIncident;
                        sqlCmd.Parameters.Add(new SqlParameter("@InOutDocument", SqlDbType.Bit)).Value = param.docIn;
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
