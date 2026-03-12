using Microsoft.Data.SqlClient;
using NGCP.BaseClass;
using NGCP.BaseModel;
using System.Data;

namespace CentralData.Class
{
    public class clsPhilDemographic
    {
        private IConfiguration _configuration;
        public clsPhilDemographic(IConfiguration configuration)
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
                        sqlCmd.CommandText = "SP_PhilDemographic_Qry";
                        sqlCmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = model._action;
                        sqlCmd.Parameters.Add(new SqlParameter("@StrParam", SqlDbType.NVarChar)).Value = model.strParam;
                        sqlCmd.Parameters.Add(new SqlParameter("@ProvCode", SqlDbType.NVarChar)).Value = model.provCode;
                        sqlCmd.Parameters.Add(new SqlParameter("@MunCode", SqlDbType.NVarChar)).Value = model.munCode;
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
    }
}
