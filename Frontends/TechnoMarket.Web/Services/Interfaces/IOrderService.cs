using TechnoMarket.Web.Models.Order;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderVM>> GetAllOrdersAsync();
        Task<List<OrderVM>> GetOrderByCustomerId(string id);
        Task<OrderVM> CreateOrderAsync(CheckoutInput checkoutInput, string customerId, string userId);

    }
}
