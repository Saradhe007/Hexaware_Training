using Microsoft.Data.SqlClient;

namespace CarConnect.Util
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(DBPropertyUtil.GetConnectionString());
        }
    }
}
