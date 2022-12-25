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

        public async Task<BasketVM> Get()
        {
            var user = _userManager.Users.FirstOrDefault();

            //Sepet dolu mu değil mi check ediyoruz.
            var response = await _httpClient.GetAsync($"baskets?userId={user.Id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var basketVM = await response.Content.ReadFromJsonAsync<CustomResponseDto<BasketVM>>();
            return basketVM.Data;
        }

        public async Task AddBasketItem(BasketItemVM basketItemVM)
        {
            var basketVM = await Get();

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
                basketVM.UserId = _userManager.Users.FirstOrDefault().Id;
                basketVM.BasketItems.Add(basketItemVM);
            }

            await SaveOrUpdate(basketVM);
            return;
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var basketVM = await Get();

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
                return await Delete();
            }

            return await SaveOrUpdate(basketVM);
        }

        public async Task<bool> SaveOrUpdate(BasketVM basketVM)
        {
            var response = await _httpClient.PostAsJsonAsync<BasketVM>("baskets", basketVM);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete()
        {
            var user = _userManager.Users.FirstOrDefault();
            var response = await _httpClient.DeleteAsync($"baskets?userId={user.Id}");
            return response.IsSuccessStatusCode;
        }


    }
}
