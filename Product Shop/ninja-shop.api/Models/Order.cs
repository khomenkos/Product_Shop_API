using System;
using System.Collections.Generic;

namespace Product_Shop.core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public double Total { get; set; }
        public IList<ProductRequest> ProductRequests { get; set; }
    }
}