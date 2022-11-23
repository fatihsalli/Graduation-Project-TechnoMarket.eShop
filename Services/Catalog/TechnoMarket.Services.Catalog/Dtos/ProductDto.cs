using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Dtos
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public string ImageFile { get; set; }
        public string ProductFeature { get; set; }
        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }


    }
}
