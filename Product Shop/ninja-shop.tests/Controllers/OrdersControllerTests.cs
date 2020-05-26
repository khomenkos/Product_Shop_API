using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ninja_shop.api.Controllers;
using ninja_shop.api.Services;
using ninja_shop.core.Models;
using NSubstitute;
using NUnit.Framework;

namespace ninja_shop.tests.Controllers
{
    [TestFixture]
    public class OrdersControllerTests
    {
        private IOrderRepository _productRepository;
        private OrdersController _controller;

        [Test]
        public void GetOrders()
        {
            _productRepository.GetOrders().Returns(new List<Order> {new Order()});

            IActionResult actionResult = _controller.GetOrders();
            var result = actionResult as OkObjectResult;
            var products = result.Value as IList<Order>;

            _productRepository.Received(1).GetOrders();
            Assert.AreEqual(1, products.Count);
        }
        
        [Test]
        public void GetOrderById()
        {
            var fakeOrder = new Order();
            _productRepository.GetOrder(Arg.Any<int>()).Returns(fakeOrder);
            _productRepository.Exists(Arg.Any<int>()).Returns(true);

            IActionResult actionResult = _controller.GetOrderById(1);
            var result = actionResult as OkObjectResult;
            var customer = result.Value as Order;

            _productRepository.Received(1).Exists(1);
            _productRepository.Received(1).GetOrder(1);
            Assert.AreSame(fakeOrder, customer);
        }
        
        [SetUp]
        public void Setup()
        {
            _productRepository = Substitute.For<IOrderRepository>();
            _controller = new OrdersController(_productRepository);
        }
    }
}