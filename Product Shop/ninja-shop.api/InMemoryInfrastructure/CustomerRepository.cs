using System.Collections.Generic;
using System.Linq;
using Product_Shop.api.Services;
using Product_Shop.core.Models;

namespace Product_Shop.api.InMemoryInfrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDataContext _dataContext;

        public CustomerRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Exists(int customerId)
        {
            return _dataContext.Customers.Any(x => x.Id == customerId);
        }

        public IList<Order> GetOrders(int customerId)
        {
            return _dataContext.Orders.Where(x => x.CustomerId == customerId).ToList();
        }

        public Customer GetCustomer(int customerId)
        {
            return _dataContext.Customers.SingleOrDefault(x => x.Id == customerId);
        }

        public Customer CreateCustomer(Customer customer)
        {
            customer.Id =  _dataContext.Customers.DefaultIfEmpty(new Customer()).Max(x => x.Id) + 1;
            _dataContext.Customers.Add(customer);

            return customer;
        }

        public IList<Customer> GetCustomers()
        {
            return _dataContext.Customers;
        }
    }
}