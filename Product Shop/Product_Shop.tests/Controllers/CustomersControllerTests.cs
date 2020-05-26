using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ninja_shop.api.Controllers;
using ninja_shop.api.Helpers;
using ninja_shop.api.Services;
using ninja_shop.core.Models;
using NSubstitute;
using NUnit.Framework;

namespace ninja_shop.tests.Controllers
{
    [TestFixture]
    public class CustomersControllerTests
    {
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        private CustomersController _controller;

        [Test]
        public void GetCustomers_WhenCalled_ReturnsOKWithCustomerCollection()
        {
            _customerRepository.GetCustomers().Returns(new List<Customer> {new Customer()});

            IActionResult actionResult = _controller.GetCustomers();
            var result = actionResult as OkObjectResult;
            var customers = result.Value as IList<Customer>;

            _customerRepository.Received(1).GetCustomers();
            Assert.AreEqual(1, customers.Count);
        }

        [Test]
        public void GetCustomerById()
        {
            var fakeCustomer = new Customer();
            _customerRepository.GetCustomer(Arg.Any<int>()).Returns(fakeCustomer);
            _customerRepository.Exists(Arg.Any<int>()).Returns(true);

            IActionResult actionResult = _controller.GetCustomerById(1);
            var result = actionResult as OkObjectResult;
            var customer = result.Value as Customer;

            _customerRepository.Received(1).Exists(1);
            _customerRepository.Received(1).GetCustomer(1);
            Assert.AreSame(fakeCustomer, customer);
        }

        [Test]
        public void CreateCustomer_CreateCustomerCalled()
        {
            _controller.CreateCustomer(new Customer());

            _customerRepository.Received(1).CreateCustomer(Arg.Any<Customer>());
        }

        [Test]
        public void GetCustomerById_WhenDoesntExist_ReturnsNotFound()
        {
            _customerRepository.Exists(Arg.Any<int>()).Returns(false);

            IActionResult actionResult = _controller.GetCustomerById(1);
            var result = actionResult as NotFoundResult;

            Assert.NotNull(result);
            _customerRepository.Received(1).Exists(1);
            _customerRepository.Received(0).GetCustomer(1);
        }

        [Test]
        public void GetOrders_WhenDoesntExist_ReturnsNotFound()
        {
            _customerRepository.Exists(Arg.Any<int>()).Returns(false);

            IActionResult actionResult = _controller.GetOrders(1);
            var result = actionResult as NotFoundResult;

            Assert.NotNull(result);
            _customerRepository.Received(1).Exists(1);
        }

        [Test]
        public void CreateOrder_WhenDoesntExist_ReturnsNotFound()
        {
            _customerRepository.Exists(Arg.Any<int>()).Returns(false);

            IActionResult actionResult = _controller.CreateOrder(1, new List<ProductRequest>());
            var result = actionResult as NotFoundResult;

            Assert.NotNull(result);
            _customerRepository.Received(1).Exists(1);
        }

        [SetUp]
        public void Setup()
        {
            _customerRepository = Substitute.For<ICustomerRepository>();
            _orderRepository = Substitute.For<IOrderRepository>();
            _controller = new CustomersController(_customerRepository, _orderRepository);
        }
    }
}