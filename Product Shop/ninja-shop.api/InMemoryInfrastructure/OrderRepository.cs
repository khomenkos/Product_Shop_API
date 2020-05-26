using System.Collections.Generic;
using System.Linq;
using Product_Shop.api.Services;
using Product_Shop.core.Models;

namespace Product_Shop.api.InMemoryInfrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDataContext _dataContext;

        public OrderRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Exists(int orderId)
        {
            return _dataContext.Orders.Any(x => x.Id == orderId);
        }

        public Order GetOrder(int orderId)
        {
            return _dataContext.Orders.SingleOrDefault(x => x.Id == orderId);
        }

        public IList<Order> GetOrders()
        {
            return _dataContext.Orders;
        }

        public Order CreateOrder(int customerId, IList<ProductRequest> productRequests)
        {
            var order = new Order();
            order.CustomerId = customerId;
            order.ProductRequests = productRequests;
            order.Id = _dataContext.Orders.DefaultIfEmpty(new Order()).Max(x => x.Id) + 1;
            order.Total = productRequests.Sum(x => x.CurrentPrice * x.RequestCount);

            _dataContext.Orders.Add(order);
            return order;
        }
    }
}