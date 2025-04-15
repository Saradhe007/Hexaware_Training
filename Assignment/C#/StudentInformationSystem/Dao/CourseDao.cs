using System;
using System.Collections.Generic;


using StudentInformationSystem.Entity;
using Microsoft.Data.SqlClient;
using StudentInformationSystem.Util;

namespace StudentInformationSystem.Dao
{
    public class CourseDao : ICourseDao
    {
     
        private readonly SqlConnection sqlCon = DBConnUtil.GetConnection("AppSetting.json");
        private readonly SqlCommand cmd = new();
        private SqlDataReader? dr;

        public Course? GetCourseByCode(string courseCode)
        {
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = $"SELECT * FROM Courses WHERE CourseCode='{courseCode}'";

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new(
                        Convert.ToInt32(dr["CourseId"]),
                        dr["CourseName"].ToString() ?? string.Empty,
                        dr["CourseCode"].ToString() ?? string.Empty
                    );
                }
            }
            finally
            {
                sqlCon.Close();
            }

            return null;
        }

        public List<Course> GetAllCourses()
        {
            var courses = new List<Course>(); // ✅ simplified collection initialization

            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "SELECT * FROM Courses";

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    courses.Add(new(
                        Convert.ToInt32(dr["CourseId"]),
                        dr["CourseName"].ToString() ?? string.Empty,
                        dr["CourseCode"].ToString() ?? string.Empty
                    ));
                }
            }
            finally
            {
                sqlCon.Close();
            }

            return courses;
        }
    }
}

