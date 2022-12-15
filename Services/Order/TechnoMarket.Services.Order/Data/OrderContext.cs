using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using TechnoMarket.Services.Order.Data.Interfaces;
using TechnoMarket.Services.Order.Settings.Interfaces;

namespace TechnoMarket.Services.Order.Data
{
    public class OrderContext : IOrderContext
    {
        public OrderContext(IOrderDatabaseSettings orderDatabaseSettings)
        {
            #region Neden CollectionSettings Düzenledik?
            //Önemli !!! GuidRepresentation.Standard yaptığımız durumda find metodunda bulamıyordu. GuidRepresentation.CSharpLegacy yapmamızı bekliyordu. O sebeple bu ayarlamayı yaptık. Global anlamda bunu yapmak için "Global.asax.cs" dosyası oluşturarak yapmamız gerekiyordu. Onun yerine aşağıda tanımladık. Sorunsuz çalışıyor. 
            #endregion
            var collectionSettings = new MongoCollectionSettings { GuidRepresentation = GuidRepresentation.Standard };

            var client = new MongoClient(orderDatabaseSettings.ConnectionString);

            var database = client.GetDatabase(orderDatabaseSettings.DatabaseName);

            Orders = database.GetCollection<Models.Order>(orderDatabaseSettings.OrderCollectionName,collectionSettings);

            //OrderContextSeed.SeedData(Orders);
        }

        public IMongoCollection<Models.Order> Orders { get; }
    }
}
