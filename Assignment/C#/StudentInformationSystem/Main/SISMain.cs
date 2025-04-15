using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;
using StudentInformationSystem.Dao;
using StudentInformationSystem.Exceptions;
using StudentInformationSystem.Util;
using Microsoft.Data.SqlClient;

namespace StudentInformationSystem.Main
{
    public class SISMain
    {
        static void Main(string[] args)
        {
            IStudDao studentDAO = new StudDao();
            ICourseDao courseDAO = new CourseDao();
            ITeacherDao teacherDAO = new TeacherDao();
            IEnrollmentDao enrollmentDAO = new EnrollmentDao();
            IPaymentDao paymentDAO = new PaymentDao();

            while (true)
            {
                Console.WriteLine("\n====== Student Information System ======");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Course");
                Console.WriteLine("3. Add Teacher");
                Console.WriteLine("4. Enroll Student in Course");
                Console.WriteLine("5. Record Payment");
                Console.WriteLine("6. Assign Teacher to Course");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("\n--- Add Student ---");

                            Console.Write("Student ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int studentId))
                                throw new Exception("Invalid Student ID.");

                            Console.Write("First Name: ");
                            string? fname = Console.ReadLine();

                            Console.Write("Last Name: ");
                            string? lname = Console.ReadLine();

                            Console.Write("DOB (yyyy-mm-dd): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dob))
                                throw new Exception("Invalid Date.");

                            Console.Write("Email: ");
                            string? email = Console.ReadLine();

                            Console.Write("Phone Number: ");
                            string? phone = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(fname) || string.IsNullOrWhiteSpace(lname) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
                                throw new Exception("Fields cannot be empty.");

                            var student = new Student(studentId, fname, lname, dob, email, phone);
                            string insertStudentQuery = $"INSERT INTO Student VALUES ({student.StudentId}, '{student.FirstName}', '{student.LastName}', '{student.DateOfBirth:yyyy-MM-dd}', '{student.Email}', '{student.PhoneNumber}')";
                            ExecuteNonQuery(insertStudentQuery);
                            Console.WriteLine("✅ Student added successfully.");
                            break;

                        case 2:
                            Console.WriteLine("\n--- Add Course ---");
                            Console.Write("Course ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int courseId))
                                throw new Exception("Invalid Course ID.");

                            Console.Write("Course Name: ");
                            string? courseName = Console.ReadLine();

                            Console.Write("Course Code: ");
                            string? courseCode = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(courseName) || string.IsNullOrWhiteSpace(courseCode))
                                throw new Exception("Course fields cannot be empty.");

                            var course = new Course(courseId, courseName, courseCode);
                            string insertCourseQuery = $"INSERT INTO Course (CourseId, CourseName, CourseCode) VALUES ({course.CourseId}, '{course.CourseName}', '{course.CourseCode}')";
                            ExecuteNonQuery(insertCourseQuery);
                            Console.WriteLine("✅ Course added successfully.");
                            break;

                        case 3:
                            Console.WriteLine("\n--- Add Teacher ---");

                            Console.Write("Teacher ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int teacherId))
                                throw new Exception("Invalid Teacher ID.");

                            Console.Write("First Name: ");
                            string? tFname = Console.ReadLine();

                            Console.Write("Last Name: ");
                            string? tLname = Console.ReadLine();

                            Console.Write("Email: ");
                            string? tEmail = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(tFname) || string.IsNullOrWhiteSpace(tLname) || string.IsNullOrWhiteSpace(tEmail))
                                throw new Exception("Teacher fields cannot be empty.");

                            var teacher = new Teacher(teacherId, tFname, tLname, tEmail);
                            string insertTeacherQuery = $"INSERT INTO Teacher (TeacherId, FirstName, LastName, Email) VALUES ({teacher.TeacherId}, '{teacher.FirstName}', '{teacher.LastName}', '{teacher.Email}')";
                            ExecuteNonQuery(insertTeacherQuery);
                            Console.WriteLine("✅ Teacher added successfully.");
                            break;

                        case 4:
                            Console.WriteLine("\n--- Enroll Student in Course ---");

                            Console.Write("Enrollment ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int enrollId))
                                throw new Exception("Invalid Enrollment ID.");

                            Console.Write("Student ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int stuId))
                                throw new Exception("Invalid Student ID.");

                            Console.Write("Course ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int courId))
                                throw new Exception("Invalid Course ID.");

                            DateTime enrollDate = DateTime.Now;
                            string enrollQuery = $"INSERT INTO Enrollment (EnrollmentId, StudentId, CourseId, EnrollmentDate) VALUES ({enrollId}, {stuId}, {courId}, '{enrollDate:yyyy-MM-dd}')";
                            ExecuteNonQuery(enrollQuery);
                            Console.WriteLine("✅ Student enrolled in course.");
                            break;

                        case 5:
                            Console.WriteLine("\n--- Record Payment ---");

                            Console.Write("Payment ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int paymentId))
                                throw new Exception("Invalid Payment ID.");

                            Console.Write("Student ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int sid))
                                throw new Exception("Invalid Student ID.");

                            Console.Write("Amount: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                                throw new Exception("Invalid Amount.");

                            Console.Write("Payment Date (yyyy-mm-dd): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime payDate))
                                throw new Exception("Invalid Payment Date.");

                            string paymentQuery = $"INSERT INTO Payment (PaymentId, StudentId, Amount, PaymentDate) VALUES ({paymentId}, {sid}, {amount}, '{payDate:yyyy-MM-dd}')";
                            ExecuteNonQuery(paymentQuery);
                            Console.WriteLine("✅ Payment recorded successfully.");
                            break;

                        case 6:
                            Console.WriteLine("\n--- Assign Teacher to Course ---");

                            Console.Write("Course ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int assignCourseId))
                                throw new Exception("Invalid Course ID.");

                            Console.Write("Teacher ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int assignTeacherId))
                                throw new Exception("Invalid Teacher ID.");

                            string updateCourse = $"UPDATE Courses SET TeacherId={assignTeacherId} WHERE CourseId={assignCourseId}";
                            ExecuteNonQuery(updateCourse);
                            Console.WriteLine("✅ Teacher assigned to course.");
                            break;

                        case 0:
                            Console.WriteLine("Exiting... 🚪");
                            return;

                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[❌] Error: {ex.Message}");
                }
            }
        }

        

        public static void ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = DBConnUtil.GetConnection("AppSetting.json"))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[❌] SQL Execution Failed: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }


                }
            }

        }
    }
}












