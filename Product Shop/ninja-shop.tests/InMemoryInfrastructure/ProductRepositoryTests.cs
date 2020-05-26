using System.Collections.Generic;
using ninja_shop.api.InMemoryInfrastructure;
using ninja_shop.core.Models;
using NSubstitute;
using NUnit.Framework;

namespace ninja_shop.tests.InMemoryInfrastructure
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        [Test]
        public void Exists_WhenMatchingProductExists_True()
        {
            var productList = new List<Product> {new Product {Id = 1}};
            var dataContext = Substitute.For<IDataContext>();
            dataContext.Products.Returns(productList);

            var exists = new ProductRepository(dataContext).Exists(1);

            Assert.IsTrue(exists);
        }

        [Test]
        public void Exists_WhenNoMatchingProductExists_False()
        {
            var dataContext = Substitute.For<IDataContext>();

            var exists = new ProductRepository(dataContext).Exists(1);

            Assert.IsFalse(exists);
        }

        [Test]
        public void GetProducts__ReturnsProducts()
        {
            var productList = new List<Product> {new Product {Id = 1}};
            var dataContext = Substitute.For<IDataContext>();
            dataContext.Products.Returns(productList);

            var products = new ProductRepository(dataContext).GetProducts();

            Assert.That(products.Count == 1);
        }
        
        [Test]
        public void GetProduct_WhenExists_ReturnsProduct()
        {
            var product = new Product {Id = 1};
            var productList = new List<Product> {product};
            var dataContext = Substitute.For<IDataContext>();
            dataContext.Products.Returns(productList);

            var returnProduct = new ProductRepository(dataContext).GetProduct(1);

            Assert.AreSame(product, returnProduct);
        }
        
        [Test]
        public void GetProduct_WhenNoneExist_ReturnsNull()
        {
            var dataContext = Substitute.For<IDataContext>();
            dataContext.Products.Returns(new List<Product>());

            var returnProduct = new ProductRepository(dataContext).GetProduct(1);

            Assert.IsNull(returnProduct);
        }
    }
}