using Microsoft.Data.SqlClient;
using NGCP.BaseClass;
using NGCP.BaseModel;
using NGCP.LIS_NT.Models;
using System.Data;

namespace NGCP.LIS_NT.Class
{
    public class clsCaseHeader
    {
        private IConfiguration? _configuration;
        public clsCaseHeader(IConfiguration configuration)
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
                        sqlCmd.CommandText = "SP_CaseHeader_Qry";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = model._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@Intparam", SqlDbType.Int)).Value = model.intParam;
                        sqlCmd.Parameters.Add(new SqlParameter("@Strparam", SqlDbType.NVarChar)).Value = model.strParam;
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
                        sqlCmd.CommandText = "SP_CaseHeader";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = param._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@Id ", SqlDbType.Int)).Value = param.id;
                        sqlCmd.Parameters.Add(new SqlParameter("@OrgType", SqlDbType.NVarChar)).Value = param.orgType;
                        sqlCmd.Parameters.Add(new SqlParameter("@DocNumber", SqlDbType.NVarChar)).Value = param.docNumber;
                        sqlCmd.Parameters.Add(new SqlParameter("@CaseType", SqlDbType.NVarChar)).Value = param.caseType;
                        sqlCmd.Parameters.Add(new SqlParameter("@CaseNumber", SqlDbType.NVarChar)).Value = param.caseNumber;
                        sqlCmd.Parameters.Add(new SqlParameter("@CaseTitle", SqlDbType.NVarChar)).Value = param.caseTitle;
                        sqlCmd.Parameters.Add(new SqlParameter("@CaseDesc", SqlDbType.NVarChar)).Value = param.caseDesc;
                        sqlCmd.Parameters.Add(new SqlParameter("@CaseNature", SqlDbType.NVarChar)).Value = param.caseNature;
                        sqlCmd.Parameters.Add(new SqlParameter("@CaseStatus", SqlDbType.NVarChar)).Value = param.caseStatus;
                        sqlCmd.Parameters.Add(new SqlParameter("@CaseLevel", SqlDbType.NVarChar)).Value = param.caseLevel;
                        sqlCmd.Parameters.Add(new SqlParameter("@Venue", SqlDbType.NVarChar)).Value = param.venue;
                        sqlCmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Decimal)).Value = param.amount;
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


