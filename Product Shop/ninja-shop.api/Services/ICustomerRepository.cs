using System.Collections.Generic;
using Product_Shop.core.Models;

namespace Product_Shop.api.Services
{
    public interface ICustomerRepository
    {
        IList<Order> GetOrders(int customerId);
        Customer GetCustomer(int customerId);
        Customer CreateCustomer(Customer customer);
        IList<Customer> GetCustomers();
        bool Exists(int customerId);
    }
}