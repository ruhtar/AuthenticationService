﻿using AuthenticationService.Domain.Aggregates;

namespace AuthenticationService.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }

    public class OrderProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}