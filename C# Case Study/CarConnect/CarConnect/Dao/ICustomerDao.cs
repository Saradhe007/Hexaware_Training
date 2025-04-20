using CarConnect.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Dao
{
    public interface ICustomerDao
    {
        Customer GetCustomerById(int customerId);
        Customer GetCustomerByUsername(string username);
        void RegisterCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerId);
    }
}
