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
        Customer RegisterCustomer(Customer customer);
        Customer GetCustomerById(long id); 
        Customer GetCustomerByUsername(string username);
        Customer UpdateCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
    }

}
