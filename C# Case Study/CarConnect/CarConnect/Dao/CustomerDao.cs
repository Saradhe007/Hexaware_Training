
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using CarConnect.Entity;
using CarConnect.Dao;
using CarConnect.Util;

namespace CarConnect.Dao
{
    public class CustomerDao : ICustomerDao
    {
        public Customer RegisterCustomer(Customer customer)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber, Address, Username, Password, RegistrationDate) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Address, @Username, @Password, @RegistrationDate)", sqlCon))
                {
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@Username", customer.Username);
                    cmd.Parameters.AddWithValue("@Password", customer.Password);
                    cmd.Parameters.AddWithValue("@RegistrationDate", customer.RegistrationDate);

                    cmd.ExecuteNonQuery();
                    return customer;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE CustomerID = @CustomerID", sqlCon))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                                Address = reader.GetString(5),
                                Username = reader.GetString(6),
                                Password = reader.GetString(7),
                                RegistrationDate = reader.GetDateTime(8)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public Customer GetCustomerByUsername(string username)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE Username = @Username", sqlCon))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                                Address = reader.GetString(5),
                                Username = reader.GetString(6),
                                Password = reader.GetString(7),
                                RegistrationDate = reader.GetDateTime(8)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber, Address = @Address, Password = @Password WHERE CustomerId = @CustomerId", sqlCon))
                {
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@Password", customer.Password);
                    cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerId);
                    cmd.ExecuteNonQuery();
                    return customer;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Customer WHERE CustomerID = @CustomerID", sqlCon))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}
