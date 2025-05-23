﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Entity
{
    //Class created for Customer 
    public class Customer
    {
        public long CustomerId { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime RegistrationDate { get; set; }

        public Customer(int customerId, string firstName, string lastName, string email,
            string phoneNumber, string address, string username, string password, DateTime registrationDate)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Username = username;
            Password = password;
            RegistrationDate = registrationDate;
        }
        public Customer()
        {

        }


        public bool Authenticate(string password)
        {
            // Implemented authentication logic here
            return Password == password;
        }
    }
}
