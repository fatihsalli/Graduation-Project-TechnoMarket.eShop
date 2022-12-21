using TechnoMarket.Services.Order.Dtos;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderVM>> GetAllOrdersAsync();

    }
}
