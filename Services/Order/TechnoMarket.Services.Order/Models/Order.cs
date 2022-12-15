using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechnoMarket.Services.Order.Models
{
    public class Order
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid CustomerId { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public double Price { get; set; }
        public string Status { get; set; }

        //Embedded Document Pattern
        public Address Address { get; set; }
        public Product Product { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
