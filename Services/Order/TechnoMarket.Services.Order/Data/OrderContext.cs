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
            //To create UUID
            //BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            var client = new MongoClient(orderDatabaseSettings.ConnectionString);

            var database = client.GetDatabase(orderDatabaseSettings.DatabaseName);

            Orders = database.GetCollection<Models.Order>(orderDatabaseSettings.OrderCollectionName);

            //OrderContextSeed.SeedData(Orders);
        }

        public IMongoCollection<Models.Order> Orders { get; }
    }
}
