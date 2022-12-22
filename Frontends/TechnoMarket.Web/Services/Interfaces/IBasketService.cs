using TechnoMarket.Services.Basket.Dtos;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketVM> Get();
        Task AddBasketItem(BasketItemVM basketItemVM);
        Task<bool> SaveOrUpdate(BasketVM basketVM);


    }
}
