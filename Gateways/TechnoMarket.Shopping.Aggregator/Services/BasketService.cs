using TechnoMarket.Shared.Dtos;
using TechnoMarket.Shopping.Aggregator.Extensions;
using TechnoMarket.Shopping.Aggregator.Models.Basket;
using TechnoMarket.Shopping.Aggregator.Models.Customer;
using TechnoMarket.Shopping.Aggregator.Services.Interfaces;

namespace TechnoMarket.Shopping.Aggregator.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public async Task<BasketModel> GetAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"/api/baskets?userId={userId}");
            return await response.ReadContentAs<BasketModel>();
        }
    }
}
