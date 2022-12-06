using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TechnoMarket.Services.Order.Models;

namespace TechnoMarket.Services.Order.Dtos
{
    public class OrderDto
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public string FullAddress { get; set; }
        public ProductDto Product { get; set; }
    }
}
