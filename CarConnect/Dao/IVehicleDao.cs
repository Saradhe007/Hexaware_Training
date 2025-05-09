using System.Collections.Generic;
using CarConnect.Entity;

namespace CarConnect.Dao
{
    public interface IVehicleDao<T>
    {
        T AddVehicle(T vehicle);
        T UpdateVehicle(T vehicle);
        bool RemoveVehicle(long vehicleId);         
        T GetVehicleById(long vehicleId);           
        List<T> GetAvailableVehicles();
        List<T> GetAllVehicles();
    }
}
