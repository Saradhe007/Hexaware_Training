using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Entity
{
    public class Vehicle
    {
        public long
            
            VehicleId { get; set; }
        public string? Model { get; set; }
        public string? Make { get; set; }
        public string? Year { get; set; }
        public string? Color { get; set; }
        public string? RegistrationNumber { get; set; }

        public bool Availability { get; set; } // true if available, false if booked
        public decimal DailyRate { get; set; } // Daily rental rate


        public Vehicle(int vehicleId, string model, string make, string year, string color,
            string registrationNumber, bool availability, decimal dailyRate)
        {
            VehicleId = vehicleId;
            Model = model;
            Make = make;
            Year = year;
            Color = color;
            RegistrationNumber = registrationNumber;
            Availability = availability;
            DailyRate = dailyRate;
        }

        public Vehicle()
        {
        }

        
    }
}
