using TechnoMarket.Shopping.Aggregator.Models.Basket;

namespace TechnoMarket.Shopping.Aggregator.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketModel> GetAsync(string userId);

    }
}
