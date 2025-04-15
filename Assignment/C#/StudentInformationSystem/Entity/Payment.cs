using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public Student Student { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        // Constructor
        public Payment(int paymentId, Student student, decimal amount, DateTime paymentDate)
        {
            PaymentId = paymentId;
            Student = student;
            Amount = amount;
            PaymentDate = paymentDate;
        }
        // Overloaded constructor
        public Payment(Student student, decimal v, DateTime dateTime)
        {
            Student = student;
        }

        public Student GetStudent() => Student;
        public decimal GetPaymentAmount() => Amount;
        public DateTime GetPaymentDate() => PaymentDate;


    }
}
