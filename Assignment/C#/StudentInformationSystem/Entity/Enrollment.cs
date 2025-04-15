using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // Constructor
        public Enrollment(int enrollmentId, Student student, Course course, DateTime enrollmentDate)
        {
            EnrollmentId = enrollmentId;
            Student = student;
            Course = course;
            EnrollmentDate = enrollmentDate;
        }
        // Constructor overload (IEnrollmentDao)
        public Enrollment(Student student, Course course, DateTime dateTime)
        {
            Student = student;
            Course = course;
        }

        public List<Student> GetStudents()
        {
            return Student != null ? new List<Student> { Student } : new List<Student>();
        }

        public List<Course> GetCourse()
        {

            return Course != null ? new List<Course> { Course } : new List<Course>();
        }

    }
}

