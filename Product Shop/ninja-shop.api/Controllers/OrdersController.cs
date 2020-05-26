using Microsoft.AspNetCore.Mvc;
using Product_Shop.api.Services;

namespace Product_Shop.api.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_orderRepository.GetOrders());
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public IActionResult GetOrderById(int id)
        {
            if (!_orderRepository.Exists(id))
            {
                return NotFound();
            }

            var order = _orderRepository.GetOrder(id);

            return Ok(order);
        }
    }
}