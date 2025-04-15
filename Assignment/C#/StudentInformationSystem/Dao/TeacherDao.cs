using System;
using System.Collections.Generic;
using StudentInformationSystem.Entity;
using Microsoft.Data.SqlClient;
using StudentInformationSystem.Util;

namespace StudentInformationSystem.Dao
{
    public class TeacherDao : ITeacherDao
    {
        SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader? dr;

        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "SELECT * FROM Teacher";

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Teacher t = new Teacher(
                        Convert.ToInt32(dr["TeacherId"]),
                        dr["FirstName"]?.ToString() ?? string.Empty,
                        dr["LastName"]?.ToString() ?? string.Empty,
                        dr["Email"]?.ToString() ?? string.Empty
                    );
                    teachers.Add(t);
                }
            }
            finally
            {
                sqlCon.Close();
            }
            return teachers;
        }

        public Teacher? GetTeacherById(int teacherId)
        {
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = $"SELECT * FROM Teacher WHERE TeacherId={teacherId}";

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return new Teacher(
                        Convert.ToInt32(dr["TeacherId"]),
                        dr["FirstName"]?.ToString() ?? string.Empty,
                        dr["LastName"]?.ToString() ?? string.Empty,
                        dr["Email"]?.ToString() ?? string.Empty
                    );
                }
            }
            finally
            {
                sqlCon.Close();
            }
            return null;
        }
    }
}
