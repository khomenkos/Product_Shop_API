using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Product_Shop.api.Services;
using Product_Shop.core.Models;

namespace Product_Shop.api.InMemoryInfrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDataContext _dataContext;

        public ProductRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Exists(int productId)
        {
            return _dataContext.Products.Any(x => x.Id == productId);
        }

        public Product GetProduct(int productId)
        {
            return _dataContext.Products.SingleOrDefault(x => x.Id == productId);
        }

        public IList<Product> GetProducts()
        {
            return _dataContext.Products;
        }
    }
}