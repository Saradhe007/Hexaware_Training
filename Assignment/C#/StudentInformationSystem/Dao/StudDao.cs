using System;
using System.Collections.Generic;
using StudentInformationSystem.Entity;
using Microsoft.Data.SqlClient;
using StudentInformationSystem.Util;
namespace StudentInformationSystem.Dao
{
    public class StudDao : IStudDao
    {
        SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader? dr;

        public bool ValidateStudentLogin(string email, string password)
        {
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = $"SELECT * FROM Students WHERE Email='{email}' AND Password='{password}'";

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                dr = cmd.ExecuteReader();
                return dr.HasRows;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "SELECT * FROM Students";

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Student s = new Student( Convert.ToInt32(dr["StudentId"]),
                    dr["FirstName"]?.ToString() ?? string.Empty,
                    dr["LastName"]?.ToString() ?? string.Empty,
                    dr["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(dr["DateOfBirth"]) : DateTime.MinValue,
                    dr["Email"]?.ToString() ?? string.Empty,
                    dr["PhoneNumber"]?.ToString() ?? string.Empty

                    );
                    students.Add(s);
                }
            }
            finally
            {
                sqlCon.Close();
            }
            return students;
        }
    }

}

