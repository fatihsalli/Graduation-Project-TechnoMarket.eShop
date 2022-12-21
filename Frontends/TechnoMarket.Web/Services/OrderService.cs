using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Order.Dtos;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Services
{
    public class OrderService:IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //=> For Order
        public async Task<List<OrderVM>> GetAllOrdersAsync()
        {
            var response = await _httpClient.GetAsync("orders");

            if (!response.IsSuccessStatusCode)
            {
                //Exception-loglama
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<OrderVM>>>();
            return responseSuccess.Data;
        }



    }
}
