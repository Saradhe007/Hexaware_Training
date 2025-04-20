using CarConnect.Entity;
using CarConnect.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CarConnect.Dao
{
    public class CustomerDao : ICustomerDao
    {
        private readonly string _connectionString;

        public CustomerDao()
        {
            _connectionString = DBPropertyUtil.GetConnectionString();
        }

        public void RegisterCustomer(Customer customer)
        {
            string query = @"INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber, Username, Password, DateOfBirth, JoinDate)
                             VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Username, @Password, @DateOfBirth, @JoinDate)";

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
            cmd.Parameters.AddWithValue("@LastName", customer.LastName);
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
            cmd.Parameters.AddWithValue("@Username", customer.Username);
            cmd.Parameters.AddWithValue("@Password", customer.Password);
            cmd.Parameters.AddWithValue("@RegistrationDate", customer.RegistrationDate);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public Customer? GetCustomerById(int customerId)
        {
            string query = "SELECT * FROM Customer WHERE CustomerId = @CustomerId";

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapToCustomer(reader);
            }

            return null;
        }

        public Customer? GetCustomerByUsername(string username)
        {
            string query = "SELECT * FROM Customer WHERE Username = @Username";

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapToCustomer(reader);
            }

            return null;
        }

        public List<Customer> GetAllCustomers()
        {
            string query = "SELECT * FROM Customer";
            List<Customer> customers = new();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                customers.Add(MapToCustomer(reader));
            }

            return customers;
        }

        public void UpdateCustomer(Customer customer)
        {
            string query = @"UPDATE Customer 
                             SET FirstName = @FirstName, LastName = @LastName, Email = @Email, 
                                 PhoneNumber = @PhoneNumber, Password = @Password, 
                                 DateOfBirth = @DateOfBirth 
                             WHERE CustomerId = @CustomerId";

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
            cmd.Parameters.AddWithValue("@LastName", customer.LastName);
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
            cmd.Parameters.AddWithValue("@Password", customer.Password);
            cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteCustomer(int customerId)
        {
            string query = "DELETE FROM Customer WHERE CustomerId = @CustomerId";

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        private Customer MapToCustomer(SqlDataReader reader)
        {
            return new Customer
            {
                CustomerId = Convert.ToInt32(reader["CustomerId"]),
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Email = reader["Email"].ToString(),
                PhoneNumber = reader["PhoneNumber"].ToString(),
                Username = reader["Username"].ToString(),
                Password = reader["Password"].ToString(),
                RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"])
            };
        }
    }
}