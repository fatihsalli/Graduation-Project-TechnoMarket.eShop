using MongoDB.Driver;

namespace TechnoMarket.Services.Order.Data.Interfaces
{
    public interface IOrderContext
    {
        IMongoCollection<Models.Order> Orders { get; }
    }
}
