using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Course
    {
        private int v1;
        private string? v2;
        private string? v3;

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string InstructorName { get; set; }
        public Teacher? AssignedTeacher { get; set; }         
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        // Constructor
        public Course (int courseId, string courseName, string courseCode, string instructorName)
        {
            CourseId = courseId;
            CourseName = courseName;
            CourseCode = courseCode;
            InstructorName = instructorName;
        }

        public Course(int courseId, string courseName, string courseCode, string instructorName, string? v) : this(courseId, courseName, courseCode, instructorName)
        {
        }

        public Course(int v1, string? v2, string? v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;

            CourseName = string.Empty;
            CourseCode = string.Empty;
            InstructorName = string.Empty;
        }

        public void AssignTeacher(Teacher teacher)
        {
            AssignedTeacher = teacher;
            teacher.AssignedCourses.Add(this);
        }

        public void UpdateCourseInfo(string courseName, string courseCode, string instructorName)
        {
            CourseName = courseName;
            CourseCode = courseCode;
            InstructorName = instructorName;
        }

        public void DisplayCourseInfo()
        {
            Console.WriteLine($"Course ID: {CourseId}");
            Console.WriteLine($"Course Name: {CourseName}");
            Console.WriteLine($"Course Code: {CourseCode}");
            Console.WriteLine($"Instructor Name: {InstructorName}");
            Console.WriteLine("Enrollments:");
        }

        public List<Enrollment> GetEnrollments()
        {
            return Enrollments;
        }

        public List<Teacher> GetTeacher()
        {
            return AssignedTeacher != null ? new List<Teacher> { AssignedTeacher } : new List<Teacher>();
        }

    }
}
