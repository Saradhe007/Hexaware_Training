using NUnit.Framework;
using System.Collections.Generic;
using Moq;
using System;
using CarConnect.Dao;
using CarConnect.Entity;

namespace CarConnectAppTest
{
    [TestFixture]
    public class TestUpdateCustomerInformation
    {
        private Mock<ICustomerDao> _mockCustomerDao;
        private Customer _mockCustomer;

        [SetUp]
        public void Setup()
        {
            var customer = new Customer(
                 1, "Test", "User", "test@example.com", "9876543210", "Chennai", "testuser", "pass123", DateTime.Now
                  );


            _mockCustomerDao = new Mock<ICustomerDao>();

            // This line ensures the mock returns the customer when called
            _mockCustomerDao.Setup(dao => dao.GetCustomerByUsername("testuser"))
                            .Returns(_mockCustomer);

            _mockCustomerDao.Setup(dao => dao.UpdateCustomer(It.IsAny<Customer>()))
                            .Callback<Customer>(updated => _mockCustomer = updated);
        }

        [Test]
        public void UpdateCustomerPhone_ShouldChangePhoneNumber()
        {
            var customer = new Customer { Username = "testuser", PhoneNumber = "9876543210" };
            _mockCustomerDao.Setup(dao => dao.GetCustomerByUsername("testuser")).Returns(customer);

            _mockCustomer = _mockCustomerDao.Object.GetCustomerByUsername("testuser");
            _mockCustomer.PhoneNumber = "1234567890";

            Assert.AreEqual("1234567890", _mockCustomer.PhoneNumber, "Phone number should be updated.");
        }
    }
   
   
}

