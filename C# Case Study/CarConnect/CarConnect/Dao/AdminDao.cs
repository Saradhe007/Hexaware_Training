using CarConnect.Entity;
using CarConnect.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CarConnect.Dao
{
    public class AdminDao : IAdminDao
    {
        private readonly string _connectionString;

        public AdminDao()
        {
            _connectionString = DBPropertyUtil.GetConnectionString();
        }

        public void RegisterAdmin(Admin admin)
        {
            string query = "INSERT INTO Admin (FirstName, LastName, Email, PhoneNumber, Username, Password, Role, JoinDate) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Username, @Password, @Role, @JoinDate)";

            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
                cmd.Parameters.AddWithValue("@LastName", admin.LastName);
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", admin.PhoneNumber);
                cmd.Parameters.AddWithValue("@Username", admin.Username);
                cmd.Parameters.AddWithValue("@Password", admin.Password);
                cmd.Parameters.AddWithValue("@Role", admin.Role);
                cmd.Parameters.AddWithValue("@JoinDate", admin.JoinDate);

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Admin GetAdminById(int adminId)
        {
            string query = "SELECT * FROM Admin WHERE AdminID = @AdminID";

            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@AdminID", adminId);
                _conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Admin admin = null;
                if (reader.Read())
                {
                    admin = MapToAdmin(reader);
                }

                return admin;
            }
        }

        public Admin GetAdminByUsername(string username)
        {
            string query = "SELECT * FROM Admin WHERE Username = @Username";

            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@Username", username);
                _conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Admin admin = null;
                if (reader.Read())
                {
                    admin = MapToAdmin(reader);
                }

                return admin;
            }
        }

        public void UpdateAdmin(Admin admin)
        {
            string query = "UPDATE Admin SET FirstName=@FirstName, LastName=@LastName, Email=@Email, PhoneNumber=@PhoneNumber, Password=@Password, Role=@Role WHERE AdminID=@AdminID";

            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
                cmd.Parameters.AddWithValue("@LastName", admin.LastName);
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", admin.PhoneNumber);
                cmd.Parameters.AddWithValue("@Password", admin.Password);
                cmd.Parameters.AddWithValue("@Role", admin.Role);
                cmd.Parameters.AddWithValue("@AdminID", admin.AdminId);

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAdmin(int adminId)
        {
            string query = "DELETE FROM Admin WHERE AdminID = @AdminID";

            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@AdminID", adminId);
                _conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Admin MapToAdmin(SqlDataReader reader)
        {
            return new Admin
            {
                AdminId = Convert.ToInt32(reader["AdminID"]),
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Email = reader["Email"].ToString(),
                PhoneNumber = reader["PhoneNumber"].ToString(),
                Username = reader["Username"].ToString(),
                Password = reader["Password"].ToString(),
                Role = reader["Role"].ToString(),
                JoinDate = Convert.ToDateTime(reader["JoinDate"])
            };
        }
    }
}
