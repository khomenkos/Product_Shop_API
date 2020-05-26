using System.Collections.Generic;
using Product_Shop.core.Models;

namespace Product_Shop.api.InMemoryInfrastructure
{
    public interface IDataContext
    {
        IList<Product> Products { get; }
        IList<Order> Orders { get; }
        IList<Customer> Customers { get; }
    }
}