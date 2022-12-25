using TechnoMarket.Web.Models.Basket;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketVM> GetAsync(string id);
        Task AddBasketItemAsycn(BasketItemVM basketItemVM, string userId);
        Task<bool> RemoveBasketItemAsycn(string productId, string userId);
        Task<bool> SaveOrUpdateAsycn(BasketVM basketVM);
        Task<bool> DeleteAsycn(string id);


    }
}
