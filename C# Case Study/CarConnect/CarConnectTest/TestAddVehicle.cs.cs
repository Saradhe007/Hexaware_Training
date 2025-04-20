using System.Collections.Generic;
using NUnit.Framework;
using Moq;

using CarConnect.Dao;
using CarConnect.Entity;

namespace CarConnectAppTest
{
    [TestFixture]
    public class TestAddVehicle
    {
        private Mock<IVehicleDao> _mockVehicleDao;
        private List<Vehicle> _mockDB;

        [SetUp]
        public void Setup()
        {
            _mockDB = new List<Vehicle>();
            _mockVehicleDao = new Mock<IVehicleDao>();

            // Fix: Use the correct method signature for Moq's Setup and Returns
            _mockVehicleDao.Setup(dao => dao.AddVehicle(It.IsAny<Vehicle>()))
                .Callback((Vehicle vehicle) =>
                {
                    if (!_mockDB.Exists(v => v.VehicleId == vehicle.VehicleId))
                    {
                        _mockDB.Add(vehicle);
                    }
                });
                _mockVehicleDao.Setup(dao => dao.GetVehicleById(It.IsAny<int>()))
                .Returns((Vehicle vehicle) =>
                {
                    return _mockDB.Find(v => v.VehicleId == vehicle.VehicleId);
                    _mockDB.Add(vehicle);
                    return vehicle;
                });

        }

        [Test]
        public void AddVehicle_ShouldWorkForUniqueVehicle()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                VehicleId = 1,
                Make = "Honda",
                Model = "Civic",
                Year = "2022",
                Color = "White",
                RegistrationNumber = "TN01XX1234",
                Availability = true,
                DailyRate = 800
            };
            _mockVehicleDao.Object.AddVehicle(vehicle);

            // Assert
            var addedVehicle = _mockDB.Find(v => v.VehicleId == vehicle.VehicleId);
            Assert.That(addedVehicle, Is.Not.Null, "The vehicle should be added successfully.");
            Assert.That(addedVehicle.VehicleId, Is.EqualTo(1), "Vehicle ID should match.");
        }


        [Test]
        public void AddVehicle_Duplicate_ShouldNotAddAgain()
        {
            var vehicle = new Vehicle
            {
                VehicleId = 1,
                Make = "Honda",
                Model = "Civic",
                Year = "2022",
                Color = "White",
                RegistrationNumber = "TN01XX1234",
                Availability = true,
                DailyRate = 800
            };

            _mockVehicleDao.Object.AddVehicle(vehicle);

            var duplicate = _mockDB.FindAll(v => v.VehicleId == vehicle.VehicleId);

            Assert.That(duplicate.Count, Is.EqualTo(1));
        }
    }
}
