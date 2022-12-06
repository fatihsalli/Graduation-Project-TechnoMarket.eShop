using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechnoMarket.Services.Order.Models
{
    public class Product
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}
