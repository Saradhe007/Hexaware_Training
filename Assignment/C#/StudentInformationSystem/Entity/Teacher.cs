using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Course> AssignedCourses { get; set; } = new List<Course>();

        // Constructor

        public Teacher(int teacherId, string firstName, string lastName, string email)
        {
            TeacherId = teacherId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void UpdateTeacherInfo(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        public List<Course> GetAssignedCourse()
        {
            return AssignedCourses;
        }

    }
}
