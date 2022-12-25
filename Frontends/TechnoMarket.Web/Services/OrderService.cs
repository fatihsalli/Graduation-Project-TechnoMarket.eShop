using TechnoMarket.Shared.Dtos;
using TechnoMarket.Web.Models;
using TechnoMarket.Web.Models.Order;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;

        public OrderService(HttpClient httpClient, IBasketService basketService)
        {
            _httpClient = httpClient;
            _basketService = basketService;
        }

        //=> For Order (Admin Area)
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

        public async Task<OrderVM> CreateOrder(CheckoutInput checkoutInput)
        {
            var basket = await _basketService.Get();

            var orderCreateInput = new OrderCreateInput()
            {
                //CustomerId = basket.CustomerId,
                Address = new AddressVM()
                {
                    City = checkoutInput.City,
                    AddressLine = checkoutInput.AddressLine,
                    CityCode = checkoutInput.CityCode,
                    Country = checkoutInput.Country,
                },
                Status = "Active",
                TotalPrice = basket.TotalPrice
            };

            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemVM
                {
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity
                };
                orderCreateInput.OrderItems.Add(orderItem);
            });

            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);

            var responseSuccess = await response.Content.ReadFromJsonAsync<CustomResponseDto<OrderVM>>();

            return responseSuccess.Data;
        }





    }
}
