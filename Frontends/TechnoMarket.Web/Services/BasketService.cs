using TechnoMarket.Web.Models.Basket;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BasketVM> Get()
        {
            string customerId = "60ca5f4d-71f9-4d9d-b074-393158bda67a";

            //Sepet dolu mu değil mi check ediyoruz.
            var response = await _httpClient.GetAsync($"baskets?customerId={customerId}");

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
                basketVM.CustomerId = "60ca5f4d-71f9-4d9d-b074-393158bda67a";
                basketVM.BasketItems.Add(basketItemVM);
            }

            await SaveOrUpdate(basketVM);
            return;
        }

        public async Task<bool> SaveOrUpdate(BasketVM basketVM)
        {
            var response = await _httpClient.PostAsJsonAsync<BasketVM>("baskets", basketVM);
            return response.IsSuccessStatusCode;
        }




    }
}
