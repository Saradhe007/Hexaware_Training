using Microsoft.Data.SqlClient;
using StudentInformationSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Util
{
        public class DBConnUtil
        {
        public static SqlConnection GetConnection(string configFile)
        {
            string connstr = DBPropertyUtil.GetConnectionString(configFile);
            return new SqlConnection(connstr);
        }
    }
}
