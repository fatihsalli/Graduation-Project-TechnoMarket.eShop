using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechnoMarket.Services.Order.Models
{
    public class Product
    {
        [BsonGuidRepresentation(GuidRepresentation.CSharpLegacy)]
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}
