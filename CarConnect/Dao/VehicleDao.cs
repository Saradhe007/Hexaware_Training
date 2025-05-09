using System;
using System.Collections.Generic;
using CarConnect.Entity;
using CarConnect.Util;
using Microsoft.Data.SqlClient;

namespace CarConnect.Dao
{
    public class VehicleDao : IVehicleDao<Vehicle>
    {
        public Vehicle AddVehicle(Vehicle vehicle)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using SqlCommand cmd = new SqlCommand(@"INSERT INTO Vehicle (Model, Make, Year, Color, RegistrationNumber, Availability, DailyRate)
                                                        OUTPUT INSERTED.VehicleId
                                                        VALUES (@Model, @Make, @Year, @Color, @RegistrationNumber, @Availability, @DailyRate)", sqlCon);
                cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                cmd.Parameters.AddWithValue("@Make", vehicle.Make);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Color", vehicle.Color);
                cmd.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
                cmd.Parameters.AddWithValue("@Availability", vehicle.Availability ? 1 : 0);
                cmd.Parameters.AddWithValue("@DailyRate", vehicle.DailyRate);

                vehicle.VehicleId = Convert.ToInt64(cmd.ExecuteScalar()); // Fixed
                return vehicle;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public Vehicle GetVehicleById(long vehicleId)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using SqlCommand cmd = new SqlCommand("SELECT * FROM Vehicle WHERE VehicleId = @VehicleId", sqlCon);
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return MapToVehicle(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public List<Vehicle> GetAvailableVehicles()
        {
            var list = new List<Vehicle>();
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using SqlCommand cmd = new SqlCommand("SELECT * FROM Vehicle WHERE Availability = 1", sqlCon);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(MapToVehicle(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return list;
        }

        public Vehicle UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using SqlCommand cmd = new SqlCommand(@"UPDATE Vehicle SET 
                    Model = @Model, Make = @Make, Year = @Year, Color = @Color, 
                    RegistrationNumber = @RegistrationNumber, Availability = @Availability, 
                    DailyRate = @DailyRate WHERE VehicleId = @VehicleId", sqlCon);
                cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                cmd.Parameters.AddWithValue("@Make", vehicle.Make);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Color", vehicle.Color);
                cmd.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
                cmd.Parameters.AddWithValue("@Availability", vehicle.Availability ? 1 : 0);
                cmd.Parameters.AddWithValue("@DailyRate", vehicle.DailyRate);
                cmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);

                cmd.ExecuteNonQuery();
                return vehicle;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public bool RemoveVehicle(long vehicleId)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using SqlCommand cmd = new SqlCommand("DELETE FROM Vehicle WHERE VehicleId = @VehicleId", sqlCon);
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public List<Vehicle> GetAllVehicles()
        {
            var list = new List<Vehicle>();
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using SqlCommand cmd = new SqlCommand("SELECT * FROM Vehicle", sqlCon);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(MapToVehicle(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return list;
        }

        private Vehicle MapToVehicle(SqlDataReader reader)
        {
            return new Vehicle
            {
                VehicleId = Convert.ToInt64(reader["VehicleId"]),
                Model = reader["Model"].ToString(),
                Make = reader["Make"].ToString(),
                Year = reader["Year"].ToString(),
                Color = reader["Color"].ToString(),
                RegistrationNumber = reader["RegistrationNumber"].ToString(),
                Availability = Convert.ToBoolean(reader["Availability"]),
                DailyRate = Convert.ToDecimal(reader["DailyRate"])
            };
        }

        public bool RemoveVehicle(int vehicleId) => throw new NotImplementedException();
        public Vehicle GetVehicleById(int vehicleId) => throw new NotImplementedException();
    }
}
