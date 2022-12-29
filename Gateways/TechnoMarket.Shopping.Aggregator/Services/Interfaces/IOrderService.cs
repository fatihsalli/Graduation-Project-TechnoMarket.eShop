using TechnoMarket.Shopping.Aggregator.Models.Order;

namespace TechnoMarket.Shopping.Aggregator.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderModel> CreateOrder(OrderCreateModel orderCreateModel);
    }
}
