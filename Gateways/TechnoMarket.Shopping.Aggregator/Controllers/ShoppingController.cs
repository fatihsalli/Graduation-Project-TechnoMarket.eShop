using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Shared.ControllerBases;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Shopping.Aggregator.Models;
using TechnoMarket.Shopping.Aggregator.Models.Customer;
using TechnoMarket.Shopping.Aggregator.Models.Order;
using TechnoMarket.Shopping.Aggregator.Services.Interfaces;

namespace TechnoMarket.Shopping.Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : CustomBaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        public ShoppingController(ICustomerService customerService, IOrderService orderService, IBasketService basketService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOrder(CheckOutModel checkOutModel)
        {
            var customer = await _customerService.GetCustomer(checkOutModel.Email);

            if (customer == null)
            {
                var customerCreateModel = new CustomerCreateModel()
                {
                    Address = checkOutModel.Address,
                    Email = checkOutModel.Email,
                    FirstName = checkOutModel.FirstName,
                    LastName = checkOutModel.LastName
                };

                customer = await _customerService.AddAsync(customerCreateModel);
            }

            var basket = await _basketService.GetAsync(checkOutModel.UserId);

            var orderCreateInput = new OrderCreateModel()
            {
                CustomerId = customer.Id,
                Address = new AddressModel()
                {
                    City = customer.Address.City,
                    AddressLine = customer.Address.AddressLine,
                    CityCode = customer.Address.CityCode,
                    Country = customer.Address.Country,
                },
                Status = "Active",
                TotalPrice = basket.TotalPrice
            };

            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemModel
                {
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity
                };
                orderCreateInput.OrderItems.Add(orderItem);
            });

            var order = await _orderService.CreateOrder(orderCreateInput);

            return CreateActionResult(CustomResponseDto<OrderModel>.Success(201, order));
        }



    }
}
