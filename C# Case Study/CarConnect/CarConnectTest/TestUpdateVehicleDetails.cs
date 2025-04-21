using NUnit.Framework;
using Moq;

using CarConnect.Dao;
using CarConnect.Entity;

namespace CarConnectAppTest
{
    [TestFixture]
    public class TestUpdateVehicleDetails
    {
        private Mock<IVehicleDao<Vehicle>> _mockVehicleDao;
        private Vehicle _mockVehicle;

        [SetUp]
        public void Setup()
        {
            _mockVehicle = new Vehicle
            {
                VehicleId = 1,
                Model = "X5",
                Make = "BMW",
                Year = "2020",
                Color = "Black",
                RegistrationNumber = "TN09BMW9999",
                Availability = true,
                DailyRate = 1000
            };

            _mockVehicleDao = new Mock<IVehicleDao<Vehicle>>();

            // Setup GetVehicleById to return the mock vehicle
            _mockVehicleDao.Setup(dao => dao.GetVehicleById(1))
                           .Returns(_mockVehicle);

            // Setup UpdateVehicle to just replace values in _mockVehicle
            _mockVehicleDao.Setup(dao => dao.UpdateVehicle(It.IsAny<Vehicle>()))
                           .Callback<Vehicle>(v =>
                           {
                               _mockVehicle.Make = v.Make;
                               _mockVehicle.Model = v.Model;
                               _mockVehicle.DailyRate = v.DailyRate;
                               _mockVehicle.Color = v.Color;
                           });
        }

        [Test]
        public void UpdateDailyRate_ShouldReflectChange()
        {
            var vehicle = _mockVehicleDao.Object.GetVehicleById(1);
            vehicle.DailyRate = 1200;

            _mockVehicleDao.Object.UpdateVehicle(vehicle);

            Assert.AreEqual(1200, _mockVehicle.DailyRate);
        }
    }
}
