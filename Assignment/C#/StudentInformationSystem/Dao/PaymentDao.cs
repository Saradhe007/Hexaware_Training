using System;
using System.Collections.Generic;
using StudentInformationSystem.Entity;
using Microsoft.Data.SqlClient;
using StudentInformationSystem.Util;

namespace StudentInformationSystem.Dao
{
    public class PaymentDao : IPaymentDao
    {
        SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader? dr;

        public List<Payment> GetPaymentsByStudentId(int studentId)
        {
            List<Payment> payments = new List<Payment>();
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = $"SELECT * FROM Payments WHERE StudentId={studentId}";

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Payment p = new Payment(
                        new Student(Convert.ToInt32(dr["StudentId"]), "", "", DateTime.Now, "", ""),
                        Convert.ToDecimal(dr["Amount"]),
                        Convert.ToDateTime(dr["PaymentDate"])
                    );
                    payments.Add(p);
                }
            }
            finally
            {
                sqlCon.Close();
            }
            return payments;
        }

    }
}


