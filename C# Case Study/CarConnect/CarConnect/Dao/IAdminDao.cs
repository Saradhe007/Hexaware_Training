using CarConnect.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Dao
{
    public interface IAdminDao<T>
    {
        Admin RegisterAdmin(Admin admin);

        Admin GetAdminById(int adminId);
        Admin GetAdminByUsername(string username);
        Admin UpdateAdmin(Admin admin);
        bool DeleteAdmin(int adminId);
        Admin AdminLogin(string username, string password);
    }

}
