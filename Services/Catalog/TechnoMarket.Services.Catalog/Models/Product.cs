using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechnoMarket.Services.Catalog.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int Stock { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        //Embedded Document Pattern
        public ProductFeature Feature { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }

        //Mongo.Db tarafında collectionlara yansıtırken göz ardı edip program içerisinde kendimiz kullanmak için tanımladık.
        [BsonIgnore]
        public Category Category { get; set; }

    }
}
