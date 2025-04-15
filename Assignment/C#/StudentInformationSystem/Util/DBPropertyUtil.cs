using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Util
{
    public static class DBPropertyUtil
    {
        public static string GetConnectionString(string filePath)
        {
            //This code will search current working directory (debug folder) and it will load your setting file into it
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true);
            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            return connectionString ?? throw new InvalidOperationException("Connection String Not Found");
        }
    }
}


