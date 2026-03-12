using Microsoft.Data.SqlClient;

namespace NGCP.BaseClass
{
    public class DBConnection
    {

        private IConfiguration? Configuration;
        public DBConnection(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public SqlConnection AppConnection(string DBName)
        {
            string MConnManager = Configuration.GetConnectionString(DBName);
            SqlConnection conn = new SqlConnection(MConnManager);
            conn.Open();
            return conn;
        }

    }
}
