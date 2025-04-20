using System.Collections.Generic;
using CarConnect.Entity;

namespace CarConnect.Dao
{
    public interface IVehicleDao
    {
        Vehicle GetVehicleById(int vehicleId);
        List<Vehicle> GetAvailableVehicles();
        void AddVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
        void RemoveVehicle(int vehicleId);
        List<Vehicle> GetAllVehicles();  
    }
}
