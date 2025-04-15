
using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    //Task 1: Define a class named Student 
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public List<Payment> Payments { get; set; } = new List<Payment>();

        //Task 2: Define a constructor 
        public Student(int studentID, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            StudentId = studentID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
        }


        public void EnrollStudInCourse(Enrollment enrollment)
        {
            Enrollments.Add(enrollment);
        }
        public void UpdateStudentInfo(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public void MakePayment(Payment payment)
        {
            Payments.Add(payment);
        }

        public void DisplayStudentInfo(Student student)
        {
            Console.WriteLine($"Student ID: {student.StudentId}");
            Console.WriteLine($"Name: {student.FirstName} {student.LastName}");
            Console.WriteLine($"Date of Birth: {student.DateOfBirth.ToShortDateString()}");
            Console.WriteLine($"Email: {student.Email}");
            Console.WriteLine($"Phone Number: {student.PhoneNumber}");
            Console.WriteLine("Enrollments:");

        }
        public List<Course> GetEnrolledCourses() => Enrollments.Select(e => e.Course).ToList();
        public List<Payment> GetPaymentHistory() => Payments;

    }


}



