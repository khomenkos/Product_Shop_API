using System.Collections.Generic;
using Product_Shop.core.Models;

namespace Product_Shop.api.Services
{
    public interface IOrderRepository
    {
        bool Exists(int orderId);
        Order GetOrder(int orderId);
        IList<Order> GetOrders();
        Order CreateOrder(int customerId, IList<ProductRequest> productRequests);
    }
}