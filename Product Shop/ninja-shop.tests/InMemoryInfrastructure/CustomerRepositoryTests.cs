using System.Collections.Generic;
using ninja_shop.api.InMemoryInfrastructure;
using ninja_shop.core.Models;
using NSubstitute;
using NUnit.Framework;


namespace ninja_shop.tests.InMemoryInfrastructure
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        [Test]
        public void Exists_WhenMatchingCustomerExists_True()
        {
            var customerList = new List<Customer> {new Customer {Id = 1}};
            var dataContext = Substitute.For<IDataContext>();
            dataContext.Customers.Returns(customerList);

            var exists = new CustomerRepository(dataContext).Exists(1);

            Assert.IsTrue(exists);
        }

        [Test]
        public void Exists_WhenNoMatchingCustomerExists_False()
        {
            var dataContext = Substitute.For<IDataContext>();

            var exists = new CustomerRepository(dataContext).Exists(1);

            Assert.IsFalse(exists);
        }

        [Test]
        public void GetCustomers__ReturnsCustomers()
        {
            var customerList = new List<Customer> {new Customer {Id = 1}};
            var dataContext = Substitute.For<IDataContext>();
            dataContext.Customers.Returns(customerList);

            var customers = new CustomerRepository(dataContext).GetCustomers();

            Assert.That(customers.Count == 1);
        }

        [Test]
        public void GetCustomer_WhenExists_ReturnsCustomer()
        {
            var customer = new Customer {Id = 1};
            var customerList = new List<Customer> {customer};
            var dataContext = Substitute.For<IDataContext>();
            dataContext.Customers.Returns(customerList);

            var returnCustomer = new CustomerRepository(dataContext).GetCustomer(1);

            Assert.AreSame(customer, returnCustomer);
        }

        [Test]
        public void GetCustomer_WhenNoneExist_ReturnsNull()
        {
            var dataContext = Substitute.For<IDataContext>();
            dataContext.Customers.Returns(new List<Customer>());
            
            var returnCustomer = new CustomerRepository(dataContext).GetCustomer(999);
            
            Assert.IsNull(returnCustomer);
        }

        [Test]
        public void GetOrders_WhenGivenGoodIds_returnsOrdersForCustomer()
        {
            var dataContext = Substitute.For<IDataContext>();
            var orders = new List<Order>
            {
                new Order {Id = 1, CustomerId = 1},
                new Order {Id = 2, CustomerId = 1},
                new Order {Id = 3, CustomerId = 2},
            };
            dataContext.Orders.Returns(orders);

            var customerOrders = new CustomerRepository(dataContext).GetOrders(1);

            Assert.AreEqual(2, customerOrders.Count);
        }

        //we do not test a single order because you should now be going to the Order resource
    }
}