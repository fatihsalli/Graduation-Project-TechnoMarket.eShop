﻿namespace TechnoMarket.Web.Models.Order
{
    public class OrderItemVM
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
