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
    public class ProductsControllerTests
    {
        private IProductRepository _productRepository;
        private ProductsController _controller;

        [Test]
        public void GetProducts()
        {
            _productRepository.GetProducts().Returns(new List<Product> {new Product()});

            IActionResult actionResult = _controller.GetProducts();
            var result = actionResult as OkObjectResult;
            var products = result.Value as IList<Product>;

            _productRepository.Received(1).GetProducts();
            Assert.AreEqual(1, products.Count);
        }
        
        [Test]
        public void GetCustomerById()
        {
            var fakeProduct = new Product();
            _productRepository.GetProduct(Arg.Any<int>()).Returns(fakeProduct);
            _productRepository.Exists(Arg.Any<int>()).Returns(true);

            IActionResult actionResult = _controller.GetProductById(1);
            var result = actionResult as OkObjectResult;
            var customer = result.Value as Product;

            _productRepository.Received(1).Exists(1);
            _productRepository.Received(1).GetProduct(1);
            Assert.AreSame(fakeProduct, customer);
        }
        
        [SetUp]
        public void Setup()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _controller = new ProductsController(_productRepository);
        }
    }
    
 
}