using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Dao
{
    public interface IPaymentDao
    {
        List<Payment> GetPaymentsByStudentId(int studentId);
    }
}
