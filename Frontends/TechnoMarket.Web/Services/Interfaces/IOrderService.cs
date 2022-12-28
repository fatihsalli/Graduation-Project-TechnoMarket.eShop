using TechnoMarket.Web.Models.Order;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderVM>> GetAllOrdersAsync();
        Task<OrderVM> CreateOrderAsync(CheckoutInput checkoutInput, string customerId, string userId);

    }
}
