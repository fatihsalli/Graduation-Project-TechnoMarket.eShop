using TechnoMarket.Web.Models.Basket;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketVM> Get();
        Task AddBasketItem(BasketItemVM basketItemVM);
        Task<bool> RemoveBasketItem(string productId);
        Task<bool> SaveOrUpdate(BasketVM basketVM);
        Task<bool> Delete();


    }
}
