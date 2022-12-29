using TechnoMarket.Shopping.Aggregator.Extensions;
using TechnoMarket.Shopping.Aggregator.Models.Order;
using TechnoMarket.Shopping.Aggregator.Services.Interfaces;

namespace TechnoMarket.Shopping.Aggregator.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<OrderModel> CreateOrder(OrderCreateModel orderCreateModel)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/orders",orderCreateModel);
            return await response.ReadContentAs<OrderModel>();
        }
    }
}
