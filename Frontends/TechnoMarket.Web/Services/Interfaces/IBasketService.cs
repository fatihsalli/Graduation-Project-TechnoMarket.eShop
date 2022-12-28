using TechnoMarket.Web.Models.Basket;
using TechnoMarket.Web.Models.Order;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketVM> GetAsync(string id);
        Task AddBasketItemAsycn(BasketItemVM basketItemVM, string userId);
        Task<bool> RemoveBasketItemAsycn(string productId, string userId);
        Task<bool> SaveOrUpdateAsycn(BasketVM basketVM);
        Task<bool> DeleteAsycn(string id);
        Task<bool> CheckOutForAsyncCommunication(CheckoutInput checkoutInput, string customerId, string userId);


    }
}
