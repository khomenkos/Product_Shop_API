using System.Collections.Generic;
using Product_Shop.core.Models;

namespace Product_Shop.api.Services
{
    public interface IProductRepository
    {
        bool Exists(int productId);
        Product GetProduct(int productId);
        IList<Product> GetProducts();
    }
}