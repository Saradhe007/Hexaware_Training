using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace CarConnect.Util
{
    public static class DBPropertyUtil
    {
        private static readonly IConfigurationRoot _configuration;

        static DBPropertyUtil()
        {
            try
            {
                // Get path where the executable is running (bin/Debug/net8.0)
                string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                // Build configuration from appsettings.json in the output path
                _configuration = new ConfigurationBuilder()
                    .SetBasePath(exePath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Failed to load configuration.");
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public static string GetConnectionString()
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }

            return connectionString;
        }
    }
}
