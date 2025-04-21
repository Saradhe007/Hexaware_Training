using CarConnect.Entity;
using CarConnect.Util;
using CarConnect.Dao;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CarConnect.Dao
{
    public class ReservationDao : IReservationDao<Reservation>
    {
        public Reservation CreateReservation(Reservation reservation)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using SqlCommand cmd = new SqlCommand(@"INSERT INTO Reservation (CustomerId, VehicleId, StartDate, EndDate, TotalCost, Status)
                                                        OUTPUT INSERTED.ReservationID
                                                        VALUES (@CustomerId, @VehicleId, @StartDate, @EndDate, @TotalCost, @Status)", sqlCon);
                cmd.Parameters.AddWithValue("@CustomerId", reservation.CustomerId);
                cmd.Parameters.AddWithValue("@VehicleId", reservation.VehicleId);
                cmd.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", reservation.EndDate);
                cmd.Parameters.AddWithValue("@TotalCost", reservation.TotalCost);
                cmd.Parameters.AddWithValue("@Status", reservation.Status);

                reservation.ReservationId = (int)cmd.ExecuteScalar();
                return reservation;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public Reservation GetReservationById(int reservationId)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using SqlCommand cmd = new SqlCommand("SELECT * FROM Reservation WHERE ReservationId = @ReservationId", sqlCon);
                cmd.Parameters.AddWithValue("@ReservationId", reservationId);

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return MapToReservation(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public List<Reservation> GetReservationsByCustomerId(int customerId)
        {
            var list = new List<Reservation>();
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using SqlCommand cmd = new SqlCommand("SELECT * FROM Reservation WHERE CustomerId= @CustomerId", sqlCon);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(MapToReservation(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return list;
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using SqlCommand cmd = new SqlCommand(@"UPDATE Reservation SET StartDate = @StartDate, EndDate = @EndDate, TotalCost = @TotalCost, Status = @Status WHERE ReservationId = @ReservationId", sqlCon);
                cmd.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", reservation.EndDate);
                cmd.Parameters.AddWithValue("@TotalCost", reservation.TotalCost);
                cmd.Parameters.AddWithValue("@Status", reservation.Status);
                cmd.Parameters.AddWithValue("@ReservationId", reservation.ReservationId);

                cmd.ExecuteNonQuery();
                return reservation;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public bool CancelReservation(int reservationId)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using SqlCommand cmd = new SqlCommand("DELETE FROM Reservation WHERE ReservationId = @ReservationId", sqlCon);
                cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        private Reservation MapToReservation(SqlDataReader reader)
        {
            return new Reservation
            {
                ReservationId = reader.GetInt32(reader.GetOrdinal("ReservationID")),
                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                VehicleId = reader.GetInt32(reader.GetOrdinal("VehicleID")),
                StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                TotalCost = reader.GetDecimal(reader.GetOrdinal("TotalCost")),
                Status = reader["Status"].ToString() 
            };
        }

    }
}
