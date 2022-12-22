using TechnoMarket.Web.Models.Order;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderVM>> GetAllOrdersAsync();
        Task<OrderVM> CreateOrder(CheckoutInput checkoutInput);

    }
}
