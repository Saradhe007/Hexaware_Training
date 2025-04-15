using System;
using System.Collections.Generic;
using StudentInformationSystem.Entity;
using Microsoft.Data.SqlClient;
using StudentInformationSystem.Util;

namespace StudentInformationSystem.Dao
{
    public class EnrollmentDao : IEnrollmentDao
    {
        SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader? dr;

        public List<Enrollment> GetEnrollmentsByCourseId(int courseId)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = $"SELECT * FROM Enrollments WHERE CourseId={courseId}";

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Enrollment e = new Enrollment(
                        new Student(Convert.ToInt32(dr["StudentId"]), "", "", DateTime.Now, "", ""),
                        new Course(Convert.ToInt32(dr["CourseId"]), "", ""),
                        Convert.ToDateTime(dr["EnrollmentDate"])
                    );
                    enrollments.Add(e);
                }
            }
            finally
            {
                sqlCon.Close();
            }

            return enrollments;
        }
    }
}
