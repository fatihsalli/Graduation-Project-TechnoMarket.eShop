using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechnoMarket.Services.Order.Models
{
    public class Order
    {
        #region Neden ObjectId Kullanmadık?
        //Aşağıdaki gibi düzenleme yapsaydık MongoDb otomatik olarak id yi oluşturacaktı. Ancak microservis projemizde diğer servisler uuid v4 kullandığı için bu sebeple burada da uuid kullanmak için aşağıdaki gibi düzenleme yapıldı.
        //[BsonRepresentation(BsonType.ObjectId)] 
        #endregion
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid CustomerId { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        //Embedded Document Pattern
        public Address Address { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
