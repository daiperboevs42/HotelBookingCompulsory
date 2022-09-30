using HotelBooking.Core;
using HotelBooking.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class CustomerTests
    {
        private Mock<IRepository<Customer>> fakeCustomerRepository;
        private CustomersController controller;

        public CustomerTests()
        {
            var customers = new List<Customer>
            {
                new Customer { Id=1, Name="John Smith" },
                new Customer { Id=2, Name="Jane Doe" },
            };

            // Create fake customerRepository. 
            fakeCustomerRepository = new Mock<IRepository<Customer>>();

            // Implement fake GetAll() method.
            fakeCustomerRepository.Setup(x => x.GetAll()).Returns(customers);

            // Implement fake Get() method.
            fakeCustomerRepository.Setup(x => x.Get(2)).Returns(customers[1]);

            // Alternative setup with argument matchers:

            // Any integer:
            fakeCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(customers[1]);


            // Integers from 1 to 2 (using a predicate)
            // If the fake Get is called with an another argument value than 1 or 2,
            // it returns null, which corresponds to the behavior of the real
            // repository's Get method.
            fakeCustomerRepository.Setup(x => x.Get(It.Is<int>(id => id > 0 && id < 3))).Returns(customers[1]);

            // Integers from 1 to 2 (using a range)
            fakeCustomerRepository.Setup(x =>
            x.Get(It.IsInRange<int>(1, 2, Moq.Range.Inclusive))).Returns(customers[1]);


            // Create RoomsController
            controller = new CustomersController(fakeCustomerRepository.Object);

        }

        [Fact]
        public void GetAll_ReturnsListWithCorrectNumberOfCustomers()
        {
            // Act
            var result = controller.Get() as List<Customer>;
            var noOfCustomers = result.Count;

            // Assert
            Assert.Equal(2, noOfCustomers);
        }

        [Fact]
        public void GetById_CustomerExists_ReturnsIActionResultWithCustomer()
        {
            // Act
            var result = controller.Get() as ObjectResult;
            var customer = result.Value as Customer;
            var customerId = customer.Id;

            // Assert
            Assert.InRange<int>(customerId, 1, 2);

        }
        [Fact]
        public void Delete_WhenIdIsLargerThanZero_RemoveIsCalled()
        {
            // Act
            controller.
                (1);

            // Assert against the mock object
            fakeCustomerRepository.Verify(x => x.Remove(1), Times.Once);
        }

    }
}
