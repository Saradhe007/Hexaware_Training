using System;
using NUnit.Framework;
using Moq;
using CarConnect.Dao;
using CarConnect.Entity;

namespace CarConnectAppTest
{
    [TestFixture]
    public class TestCustomerAuthentication_InvalidCredentials
    {
        private Mock<ICustomerDao> _mockCustomerDao;

        [SetUp]
        public void Setup()
        {
            _mockCustomerDao = new Mock<ICustomerDao>();

            var mockCustomer = new Customer(
                        1,
                        "Test",
                        "User",
                        "test@example.com",
                        "9999999999",
                        "Chennai",
                        "testuser",
                        "correctPassword",
                        DateTime.Now
             );

            _mockCustomerDao.Setup(dao => dao.GetCustomerByUsername("testuser"))
                .Returns(mockCustomer);
        }

        [Test]
        public void Authenticate_WithWrongPassword_ShouldFail()
        {
            var customer = _mockCustomerDao.Object.GetCustomerByUsername("testuser");

            bool isAuthenticated = customer.Authenticate("wrongPassword");

            Assert.IsFalse(isAuthenticated, "Authentication should fail with incorrect password.");
        }
    }
}
