using CarConnect.Entity;
using CarConnect.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CarConnect.Dao
{
    public class VehicleDao : IVehicleDao
    {
        private readonly string _connectionString;

        public VehicleDao()
        {
            _connectionString = DBPropertyUtil.GetConnectionString();
        }
        public List<Vehicle> GetAllVehicles()
        {
            string query = "SELECT * FROM Vehicle";
            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                _conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Vehicle> vehicles = new List<Vehicle>();
                while (reader.Read())
                {
                    vehicles.Add(MapToVehicle(reader));
                }
                return vehicles;
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            string query = "INSERT INTO Vehicle (Model, Make, Year, Color, RegistrationNumber, Availability, DailyRate) VALUES (@Model, @Make, @Year, @Color, @RegistrationNumber, @Availability, @DailyRate)";

            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                cmd.Parameters.AddWithValue("@Make", vehicle.Make);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Color", vehicle.Color);
                cmd.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
                cmd.Parameters.AddWithValue("@Availability", vehicle.Availability);
                cmd.Parameters.AddWithValue("@DailyRate", vehicle.DailyRate);

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            string query = "SELECT * FROM Vehicle WHERE VehicleID = @VehicleID";

            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                _conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Vehicle vehicle = null;
                if (reader.Read())
                {
                    vehicle = MapToVehicle(reader);
                }

                return vehicle;
            }
        }

        public List<Vehicle> GetAvailableVehicles()
        {
            string query = "SELECT * FROM Vehicle WHERE Availability = 1";

            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                _conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<Vehicle> vehicles = new List<Vehicle>();
                while (reader.Read())
                {
                    vehicles.Add(MapToVehicle(reader));
                }

                return vehicles;
            }
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            string query = "UPDATE Vehicle SET Model=@Model, Make=@Make, Year=@Year, Color=@Color, RegistrationNumber=@RegistrationNumber, Availability=@Availability, DailyRate=@DailyRate WHERE VehicleID=@VehicleID";

            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                cmd.Parameters.AddWithValue("@Make", vehicle.Make);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Color", vehicle.Color);
                cmd.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
                cmd.Parameters.AddWithValue("@Availability", vehicle.Availability);
                cmd.Parameters.AddWithValue("@DailyRate", vehicle.DailyRate);
                cmd.Parameters.AddWithValue("@VehicleID", vehicle.VehicleId);

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveVehicle(int vehicleId)
        {
            string query = "DELETE FROM Vehicle WHERE VehicleID = @VehicleID";

            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                _conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Vehicle MapToVehicle(SqlDataReader reader)
        {
            return new Vehicle
            {
                VehicleId = Convert.ToInt32(reader["VehicleID"]),
                Model = reader["Model"].ToString(),
                Make = reader["Make"].ToString(),
                Year = reader["Year"].ToString(),
                Color = reader["Color"].ToString(),
                RegistrationNumber = reader["RegistrationNumber"].ToString(),
                Availability = Convert.ToBoolean(reader["Availability"]),
                DailyRate = Convert.ToDecimal(reader["DailyRate"])
            };
        }
    }
}
