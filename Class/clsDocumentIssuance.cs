using Microsoft.Data.SqlClient;
using NGCP.BaseClass;
using NGCP.BaseModel;
using NGCP.LIS_NT.Models;
using System.Data;

namespace NGCP.LIS_NT.Class
{
    public class clsDocumentIssuance
    {

        private IConfiguration? _configuration;
        public clsDocumentIssuance(IConfiguration configuration)
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
                        sqlCmd.CommandText = "SP_DocIssuance_Qry";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = model._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@Strparam", SqlDbType.NVarChar)).Value = model.strParam;
                        sqlCmd.Parameters.Add(new SqlParameter("@Intparam", SqlDbType.Int)).Value = model.intParam;
                        sqlCmd.Parameters.Add(new SqlParameter("@DateFr", SqlDbType.DateTime)).Value = model.dateFr;
                        sqlCmd.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.DateTime)).Value = model.dateTo;
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



        public string CUD(mIssuance param)
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
                        sqlCmd.CommandText = "SP_DocumentIssuance";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = param._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@Id ", SqlDbType.Int)).Value = param.id;
                        sqlCmd.Parameters.Add(new SqlParameter("@DocGuid", SqlDbType.NVarChar)).Value = param.docGuid;
                        sqlCmd.Parameters.Add(new SqlParameter("@DocIn", SqlDbType.Bit)).Value = param.docIn;
                        sqlCmd.Parameters.Add(new SqlParameter("@IncomingNo", SqlDbType.NVarChar)).Value = param.incomingNo;
                        sqlCmd.Parameters.Add(new SqlParameter("@DateReceived", SqlDbType.DateTime)).Value = param.dateReceived;
                        sqlCmd.Parameters.Add(new SqlParameter("@ASGLawyer", SqlDbType.NVarChar)).Value = param.asgLawyer;
                        sqlCmd.Parameters.Add(new SqlParameter("@DateASGD", SqlDbType.DateTime)).Value = param.dateAsgd;

                        sqlCmd.Parameters.Add(new SqlParameter("@Requestor", SqlDbType.NVarChar)).Value = param.requestor;
                        sqlCmd.Parameters.Add(new SqlParameter("@DocCategory", SqlDbType.NVarChar)).Value = param.docCategory;
                        sqlCmd.Parameters.Add(new SqlParameter("@Subject", SqlDbType.NVarChar)).Value = param.subject;
                        sqlCmd.Parameters.Add(new SqlParameter("@CourtAdmin", SqlDbType.NVarChar)).Value = param.courtAdmin;
                        sqlCmd.Parameters.Add(new SqlParameter("@Region", SqlDbType.NVarChar)).Value = param.region;
                        sqlCmd.Parameters.Add(new SqlParameter("@Particular", SqlDbType.NVarChar)).Value = param.particular;
                        sqlCmd.Parameters.Add(new SqlParameter("@DateReleased", SqlDbType.DateTime)).Value = param.dateReleased;
                        sqlCmd.Parameters.Add(new SqlParameter("@OutGoingNo", SqlDbType.NVarChar)).Value = param.outgoingNo;
                        sqlCmd.Parameters.Add(new SqlParameter("@NOD", SqlDbType.Int)).Value = param.nod;
                        sqlCmd.Parameters.Add(new SqlParameter("@RevNumber", SqlDbType.Int)).Value = param.revNumber;

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
