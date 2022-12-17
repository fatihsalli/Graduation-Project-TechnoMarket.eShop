using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechnoMarket.Services.Order.Models
{
    public class OrderItem
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

    }
}
