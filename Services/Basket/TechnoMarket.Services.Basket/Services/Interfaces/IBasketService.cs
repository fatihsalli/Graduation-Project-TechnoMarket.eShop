using TechnoMarket.Services.Basket.Dtos;

namespace TechnoMarket.Services.Basket.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasket(string customerId);
        Task<bool> SaveOrUpdate(BasketDto basketDto);
        Task<bool> Delete(string customerId);

    }
}
