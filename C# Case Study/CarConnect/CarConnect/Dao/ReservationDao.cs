using CarConnect.Entity;
using CarConnect.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CarConnect.Dao
{
    public class ReservationDao : IReservationDao
    {
        private readonly string _connectionString;

        public ReservationDao()
        {
            _connectionString = DBPropertyUtil.GetConnectionString();
        }

        public void CreateReservation(Reservation reservation)
        {
            string query = "INSERT INTO Reservation VALUES (@CustomerID, @VehicleID, @StartDate, @EndDate, @TotalCost, @Status)";
            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@CustomerID", reservation.CustomerId);
                cmd.Parameters.AddWithValue("@VehicleID", reservation.VehicleId);
                cmd.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", reservation.EndDate);
                cmd.Parameters.AddWithValue("@TotalCost", reservation.TotalCost);
                cmd.Parameters.AddWithValue("@Status", reservation.Status);

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Reservation GetReservationById(int reservationId)
        {
            string query = "SELECT * FROM Reservation WHERE ReservationID = @ReservationID";
            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                _conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Reservation reservation = null;
                if (reader.Read())
                {
                    reservation = MapToReservation(reader);
                }

                return reservation;
            }
        }

        public List<Reservation> GetReservationsByCustomerId(int customerId)
        {
            string query = "SELECT * FROM Reservation WHERE CustomerID = @CustomerID";
            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                _conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<Reservation> reservations = new List<Reservation>();
                while (reader.Read())
                {
                    reservations.Add(MapToReservation(reader));
                }

                return reservations;
            }
        }

        public void UpdateReservation(Reservation reservation)
        {
            string query = "UPDATE Reservation SET StartDate=@StartDate, EndDate=@EndDate, TotalCost=@TotalCost, Status=@Status WHERE ReservationID=@ReservationID";
            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", reservation.EndDate);
                cmd.Parameters.AddWithValue("@TotalCost", reservation.TotalCost);
                cmd.Parameters.AddWithValue("@Status", reservation.Status);
                cmd.Parameters.AddWithValue("@ReservationID", reservation.ReservationId);

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void CancelReservation(int reservationId)
        {
            string query = "DELETE FROM Reservation WHERE ReservationID = @ReservationID";
            using (SqlConnection _conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                _conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Reservation MapToReservation(SqlDataReader reader)
        {
            return new Reservation
            {
                ReservationId = Convert.ToInt32(reader["ReservationID"]),
                CustomerId = Convert.ToInt32(reader["CustomerID"]),
                VehicleId = Convert.ToInt32(reader["VehicleID"]),
                StartDate = Convert.ToDateTime(reader["StartDate"]),
                EndDate = Convert.ToDateTime(reader["EndDate"]),
                TotalCost = Convert.ToDecimal(reader["TotalCost"]),
                Status = reader["Status"].ToString()
            };
        }
    }
}
