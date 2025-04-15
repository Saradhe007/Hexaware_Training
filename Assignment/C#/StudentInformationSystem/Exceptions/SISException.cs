using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace StudentInformationSystem.Exceptions
{
    public class SISException
    {
        public class DuplicateStudentException : Exception
        {
            public DuplicateStudentException(string message) : base(message)
            {
            }
        }

        public class CourseNotFoundException : Exception
        {
            public CourseNotFoundException(string message) : base(message)
            {
            }
        }
        public class StudentNotFoundException : Exception
        {
            public StudentNotFoundException(string message) : base(message)
            {
            }
        }

        public class TeacherNotFoundException : Exception
        {
            public TeacherNotFoundException(string message) : base(message)
            {
            }
        }
        public class PaymentValidationException : Exception
        {
            public PaymentValidationException(string message) : base(message) { }
        }

        public class InvalidStudentDataException : Exception
        {
            public InvalidStudentDataException(string message) : base(message) { }


        }
        public class InvalidCourseDataException : Exception
        {
            public InvalidCourseDataException(string message) : base(message) { }
        }
        public class InvalidEnrollmentDataException : Exception
        {
            public InvalidEnrollmentDataException(string message) : base(message) { }
        }
        public class InvalidTeacherDataException : Exception
        {
            public InvalidTeacherDataException(string message) : base(message) { }
        }
        public class InsufficientFundsException : Exception
        {
            public InsufficientFundsException(string message) : base(message) { }


        }



    }






}
