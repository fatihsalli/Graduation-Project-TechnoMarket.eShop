using TechnoMarket.Services.Basket.Dtos;

namespace TechnoMarket.Services.Basket.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasket(string customerId);
        Task SaveOrUpdate(BasketDto basketDto);
        Task Delete(string customerId);

    }
}
