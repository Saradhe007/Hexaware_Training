using System;
using CarConnect.Dao;
using Microsoft.Data.SqlClient;
using CarConnect.Entity;
using CarConnect.exception;
using CarConnect.Util;

namespace CarConnect.Dao
{
    public class AdminDao : IAdminDao<Admin>
    {
        // Register a new Admin
        public Admin AdminLogin(string username, string password)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();

                // Query to check if admin with provided credentials exists
                string query = @"SELECT FirstName, LastName, Email, PhoneNumber, Username, Role, JoinDate 
                                 FROM Admin 
                                 WHERE Username = @Username AND Password = @Password";

                using SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password); // Ideally, you'd hash the password before storing/fetching

                using SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows) // If a record is found
                {
                    // Assuming we get the data from the reader and map it to an Admin object
                    reader.Read(); // Move to the first (and hopefully only) row
                    Admin admin = new Admin
                    {
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Username = reader["Username"].ToString(),
                        Role = reader["Role"].ToString(),
                        JoinDate = Convert.ToDateTime(reader["JoinDate"])
                    };

                    return admin; // Return the admin object if login is successful
                }
                else
                {
                    throw new Exception("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error during login process: " + ex.Message);
            }
        }

        // Register a new Admin (implementation)
        public Admin RegisterAdmin(Admin admin)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();

                // SQL query to insert a new Admin record into the database
                string query = @"INSERT INTO Admin 
                                 (FirstName, LastName, Email, PhoneNumber, Username, Password, Role, JoinDate)
                                 VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Username, @Password, @Role, @JoinDate)";

                using SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
                cmd.Parameters.AddWithValue("@LastName", admin.LastName);
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", admin.PhoneNumber);
                cmd.Parameters.AddWithValue("@Username", admin.Username);
                cmd.Parameters.AddWithValue("@Password", admin.Password); // Ideally, hash the password
                cmd.Parameters.AddWithValue("@Role", admin.Role);
                cmd.Parameters.AddWithValue("@JoinDate", admin.JoinDate);

                cmd.ExecuteNonQuery();
                return admin; // Return the Admin after registering it
            }
            catch (Exception ex)
            {
                // Handle the exception
                throw new Exception("Error during registration process: " + ex.Message);
            }
        }

        // Get Admin by ID
        public Admin GetAdminById(int adminId)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();

                using SqlCommand cmd = new SqlCommand("SELECT * FROM Admin WHERE AdminID = @AdminID", sqlCon);
                cmd.Parameters.AddWithValue("@AdminID", adminId);

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return MapToAdmin(reader);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            return null;
        }

        // Get Admin by Username
        public Admin GetAdminByUsername(string username)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();

                using SqlCommand cmd = new SqlCommand("SELECT * FROM Admin WHERE Username = @Username", sqlCon);
                cmd.Parameters.AddWithValue("@Username", username);

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return MapToAdmin(reader);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            return null;
        }

        // Update Admin
        public Admin UpdateAdmin(Admin admin)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();

                string query = @"UPDATE Admin SET 
                                 FirstName = @FirstName, 
                                 LastName = @LastName, 
                                 Email = @Email, 
                                 PhoneNumber = @PhoneNumber, 
                                 Username = @Username, 
                                 Password = @Password, 
                                 Role = @Role 
                                 WHERE AdminID = @AdminID";

                using SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
                cmd.Parameters.AddWithValue("@LastName", admin.LastName);
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", admin.PhoneNumber);
                cmd.Parameters.AddWithValue("@Username", admin.Username);
                cmd.Parameters.AddWithValue("@Password", admin.Password);
                cmd.Parameters.AddWithValue("@Role", admin.Role);
                cmd.Parameters.AddWithValue("@AdminID", admin.AdminId);

                cmd.ExecuteNonQuery();
                return admin;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
        }

        // Delete Admin
        public bool DeleteAdmin(int adminId)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();

                using SqlCommand cmd = new SqlCommand("DELETE FROM Admin WHERE AdminID = @AdminID", sqlCon);
                cmd.Parameters.AddWithValue("@AdminID", adminId);
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
        }

        // Map reader to Admin entity
        private Admin MapToAdmin(SqlDataReader reader)
        {
            return new Admin
            {
                AdminId = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3),
                PhoneNumber = reader.GetString(4),
                Username = reader.GetString(5),
                Password = reader.GetString(6),
                Role = reader.GetString(7),
                JoinDate = reader.GetDateTime(8)
            };
        }
    }
}