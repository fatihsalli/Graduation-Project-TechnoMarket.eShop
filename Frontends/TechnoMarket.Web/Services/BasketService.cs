using Microsoft.AspNetCore.Identity;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Web.Models.Basket;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;

        public BasketService(HttpClient httpClient, UserManager<IdentityUser> userManager)
        {
            _httpClient = httpClient;
            _userManager = userManager;
        }

        public async Task<BasketVM> GetAsync(string userId)
        {
            //Sepet dolu mu değil mi check ediyoruz.
            var response = await _httpClient.GetAsync($"baskets?userId={userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var basketVM = await response.Content.ReadFromJsonAsync<CustomResponseDto<BasketVM>>();
            return basketVM.Data;
        }

        public async Task AddBasketItemAsycn(BasketItemVM basketItemVM, string userId)
        {
            var basketVM = await GetAsync(userId);

            if (basketVM != null)
            {
                if (basketVM.BasketItems.Any(x => x.ProductId == basketItemVM.ProductId))
                {
                    var basketItem = basketVM.BasketItems
                        .Where(x => x.ProductId == basketItemVM.ProductId)
                        .SingleOrDefault();

                    basketItem.Quantity++;
                }
                else
                {
                    basketVM.BasketItems.Add(basketItemVM);
                }
            }
            else //Sepet boş ise yeni nesne türeterek ilave ediyorum.
            {
                basketVM = new BasketVM();
                basketVM.UserId = userId;
                basketVM.BasketItems.Add(basketItemVM);
            }

            await SaveOrUpdateAsycn(basketVM);
            return;
        }

        public async Task<bool> RemoveBasketItemAsycn(string productId, string userId)
        {
            var basketVM = await GetAsync(userId);

            if (basketVM == null)
            {
                return false;
            }

            var deleteBasketItem = basketVM.BasketItems.FirstOrDefault(x => x.ProductId == productId);

            if (deleteBasketItem == null) return false;

            var deleteResult = basketVM.BasketItems.Remove(deleteBasketItem);

            if (!deleteResult)
            {
                return false;
            }

            if (!basketVM.BasketItems.Any())
            {
                return await DeleteAsycn(userId);
            }

            return await SaveOrUpdateAsycn(basketVM);
        }

        public async Task<bool> SaveOrUpdateAsycn(BasketVM basketVM)
        {
            var response = await _httpClient.PostAsJsonAsync<BasketVM>("baskets", basketVM);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsycn(string userId)
        {
            var response = await _httpClient.DeleteAsync($"baskets?userId={userId}");
            return response.IsSuccessStatusCode;
        }

    }
}
