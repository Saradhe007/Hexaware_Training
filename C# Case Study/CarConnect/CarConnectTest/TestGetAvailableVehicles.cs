using System.Collections.Generic;
using NUnit.Framework;
using Moq;

using CarConnect.Dao;
using CarConnect.Entity;

namespace CarConnectAppTest
{
    [TestFixture]
    public class TestGetAvailableVehicles
    {
        private Mock<IVehicleDao<Vehicle>> _mockVehicleDao;

        [SetUp]
        public void Setup()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle { VehicleId = 1, Availability = true },
                new Vehicle { VehicleId = 2, Availability = true },
                new Vehicle { VehicleId = 3, Availability = false }
            };

            _mockVehicleDao = new Mock<IVehicleDao<Vehicle>>();
            _mockVehicleDao.Setup(dao => dao.GetAvailableVehicles()).Returns(
                vehicles.FindAll(v => v.Availability));
        }

        [Test]
        public void ShouldReturnOnlyAvailableVehicles()
        {
            var available = _mockVehicleDao.Object.GetAvailableVehicles();
            Assert.AreEqual(2, available.Count);
        }
    }
}
