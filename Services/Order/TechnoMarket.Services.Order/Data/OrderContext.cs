using MongoDB.Driver;
using TechnoMarket.Services.Order.Data.Interfaces;
using TechnoMarket.Services.Order.Models;
using TechnoMarket.Services.Order.Settings.Interfaces;

namespace TechnoMarket.Services.Order.Data
{
    public class OrderContext : IOrderContext
    {
        public OrderContext(IOrderDatabaseSettings orderDatabaseSettings)
        {
            var client= new MongoClient(orderDatabaseSettings.ConnectionString);

            var database = client.GetDatabase(orderDatabaseSettings.DatabaseName);

            Orders = database.GetCollection<Models.Order>(orderDatabaseSettings.OrderCollectionName);

            //Seed Data yazılacak
        }

        public IMongoCollection<Models.Order> Orders { get; }
    }
}
