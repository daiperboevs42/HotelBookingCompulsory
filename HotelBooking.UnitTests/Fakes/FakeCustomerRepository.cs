using HotelBooking.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.UnitTests.Fakes
{
    public class FakeCustomerRepository : IRepository<Customer>
    {
        // This field is exposed so that a unit test can validate that the
        // Add method was invoked.
        public bool addWasCalled = false;
        public void Add(Customer entity)
        {
            addWasCalled = true;
        }

        // This field is exposed so that a unit test can validate that the
        // Edit method was invoked.
        public bool editWasCalled = false;

        public void Edit(Customer entity)
        {
            editWasCalled = true;
        }

        public Customer Get(int id)
        {
            return new Customer { Id = 1, Name = "John Smith" };
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer { Id=1, Name="Jane Doe", Email = "jd@gmail.com" },
                new Customer { Id=2, Name="John Smith", Email = "js@gmail.com" },
            };
            return customers;
        }


        // This field is exposed so that a unit test can validate that the
        // Remove method was invoked.
        public bool removeWasCalled = false;
        public void Remove(int id)
        {
            removeWasCalled = true;
        }
    }
}
