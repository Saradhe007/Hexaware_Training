using System.Collections.Generic;
using CarConnect.Entity;

namespace CarConnect.Dao
{
    public interface IVehicleDao<T>
    {
        T AddVehicle(T vehicle);           
        T UpdateVehicle(T vehicle);        
        bool RemoveVehicle(int vehicleId); 
        T GetVehicleById(int vehicleId);
        List<T> GetAvailableVehicles();
        List<T> GetAllVehicles(); 
        
    }


}
