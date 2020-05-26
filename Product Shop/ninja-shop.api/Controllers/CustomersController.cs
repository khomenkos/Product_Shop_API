using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Product_Shop.api.Services;
using Product_Shop.core.Models;
using Product_Shop.api.Helper;

namespace Product_Shop.api.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CustomersController(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            return Ok(_customerRepository.GetCustomers());
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public IActionResult GetCustomerById(int id)
        {
            if (!_customerRepository.Exists(id))
            {
                return NotFound();
            }

            var product = _customerRepository.GetCustomer(id);

            return Ok(product);
        }

        [HttpPost]

        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var returnCustomer = _customerRepository.CreateCustomer(customer);

            return Ok(returnCustomer);
        }

        [HttpGet("{id}/orders")]
        public IActionResult GetOrders(int id)
        {
            if (!_customerRepository.Exists(id))
            {
                return NotFound();
            }

            var orders = _customerRepository.GetOrders(id);

            return Ok(orders);
        }

        [HttpPost("{id}/orders")]
        public IActionResult CreateOrder(int id, [FromBody] IList<ProductRequest> productRequests)
        {
            if (!_customerRepository.Exists(id))
            {
                return NotFound();
            }

            if (productRequests == null || productRequests.Count == 0)
            {
                ModelState.AddModelError(nameof(IList<ProductRequest>),
                    "You did not provide a collection of Product Requests.");
            }

            var order = _orderRepository.CreateOrder(id, productRequests);

            // adds Location to the reponse header on where to pull the full Order request
            return CreatedAtRoute("GetOrderById",
                new {controller = "orders", id = order.Id}, order);
        }
    }
}