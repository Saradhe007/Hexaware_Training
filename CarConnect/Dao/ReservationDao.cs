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
                using SqlCommand cmd = new SqlCommand(@"INSERT INTO Reservation 
                                                        (CustomerId, VehicleId, StartDate, EndDate, TotalCost, Status)
                                                        OUTPUT INSERTED.ReservationID
                                                        VALUES 
                                                        (@CustomerId, @VehicleId, @StartDate, @EndDate, @TotalCost, @Status)", sqlCon);
                cmd.Parameters.AddWithValue("@CustomerId", reservation.CustomerId);
                cmd.Parameters.AddWithValue("@VehicleId", reservation.VehicleId);
                cmd.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", reservation.EndDate);
                cmd.Parameters.AddWithValue("@TotalCost", reservation.TotalCost);
                cmd.Parameters.AddWithValue("@Status", reservation.Status);

                reservation.ReservationId =(int) Convert.ToInt64(cmd.ExecuteScalar());
                return reservation;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public Reservation GetReservationById(long reservationId)
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

        public Reservation UpdateReservation(Reservation reservation)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using SqlCommand cmd = new SqlCommand(@"UPDATE Reservation SET 
                                                        StartDate = @StartDate, 
                                                        EndDate = @EndDate, 
                                                        TotalCost = @TotalCost, 
                                                        Status = @Status 
                                                        WHERE ReservationId = @ReservationId", sqlCon);
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

        public bool CancelReservation(long reservationId)
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

        public List<Reservation> GetReservationsByCustomerId(long customerId)
        {
            var reservations = new List<Reservation>();
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("appsettings.json");
                sqlCon.Open();
                using SqlCommand cmd = new SqlCommand("SELECT * FROM Reservation WHERE CustomerId = @CustomerId", sqlCon);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reservations.Add(MapToReservation(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return reservations;
        }

        private Reservation MapToReservation(SqlDataReader reader)
        {
            return new Reservation
            {
                ReservationId = (int)Convert.ToInt64(reader["ReservationID"]),
                CustomerId = (int)Convert.ToInt64(reader["CustomerID"]),
                VehicleId = (int)Convert.ToInt64(reader["VehicleID"]),
                StartDate = Convert.ToDateTime(reader["StartDate"]),
                EndDate = Convert.ToDateTime(reader["EndDate"]),
                TotalCost = Convert.ToDecimal(reader["TotalCost"]),
                Status = reader["Status"].ToString()
            };
        }
    }
}
