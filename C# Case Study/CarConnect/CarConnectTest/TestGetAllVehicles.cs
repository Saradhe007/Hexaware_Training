using System.Collections.Generic;
using NUnit.Framework;
using Moq;

using CarConnect.Dao;
using CarConnect.Entity;

namespace CarConnectAppTest
{
    [TestFixture]
    public class TestGetAllVehicles
    {
        private Mock<IVehicleDao> _mockVehicleDao;

        [SetUp]
        public void Setup()
        {
            var allVehicles = new List<Vehicle>
            {
                new Vehicle { VehicleId = 1 },
                new Vehicle { VehicleId = 2 },
                new Vehicle { VehicleId = 3 }
            };

            _mockVehicleDao = new Mock<IVehicleDao>();
            _mockVehicleDao.Setup(dao => dao.GetAllVehicles()).Returns(allVehicles);
        }

        [Test]
        public void ShouldReturnAllVehicles()
        {
            var result = _mockVehicleDao.Object.GetAllVehicles() as List<Vehicle>;
            Assert.AreEqual(3, result?.Count); 
        }
    }
}
