using NUnit.Framework;
using Moq;

using CarConnect.Dao;
using CarConnect.Entity;

namespace CarConnectAppTest
{
    [TestFixture]
    public class TestUpdateVehicleDetails
    {
        private Mock<IVehicleDao> _mockVehicleDao;
        private Vehicle _mockVehicle;

        [SetUp]
        public void Setup()
        {
            _mockVehicle = new Vehicle
            {
                VehicleId = 1,
                Model = "X5",
                Make = "BMW",
                DailyRate = 1000
            };

            _mockVehicleDao = new Mock<IVehicleDao>();
            _mockVehicleDao.Setup(dao => dao.GetVehicleById(1)).Returns(_mockVehicle);
            _mockVehicleDao.Setup(dao => dao.UpdateVehicle(It.IsAny<Vehicle>()))
                .Callback<Vehicle>(v => _mockVehicle = v);
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
