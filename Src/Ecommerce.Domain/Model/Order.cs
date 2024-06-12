using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Model
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public DateTime DeliveryExpected { get; set; }
        public bool ContainsGift { get; set; }

    }
}
