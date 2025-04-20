using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Entity
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; } // e.g., "Pending", "Confirmed", "Cancelled"
        public decimal TotalCost { get; set; }

        public Reservation() { }
        public Reservation(int reservationId, int customerId, int vehicleId, DateTime startDate, DateTime endDate, string status)
        {
            ReservationId = reservationId;
            CustomerId = customerId;
            VehicleId = vehicleId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }
        // Method to calculate total cost based on the duration of the reservation and daily rate
        // Assuming dailyRate is passed as a parameter

        public void CalculayeTotalCost(decimal dailyRate)
        {
            TimeSpan duration = EndDate - StartDate;
            TotalCost = (decimal)duration.TotalDays * dailyRate;
        }
    }
}