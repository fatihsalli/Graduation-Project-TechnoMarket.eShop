using Microsoft.AspNetCore.Mvc;
using Moq;
using TechnoMarket.Services.Order.Controllers;
using TechnoMarket.Services.Order.Dtos;
using TechnoMarket.Services.Order.Models;
using TechnoMarket.Services.Order.Services.Interfaces;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Shared.Exceptions;
using Xunit;

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class OrdersControllerTest
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrdersController _ordersController;

        //Fake data
        private List<OrderDto> _orders;

        public OrdersControllerTest()
        {
            _mockOrderService = new Mock<IOrderService>();
            _ordersController = new OrdersController(_mockOrderService.Object);

            _orders = new List<OrderDto>()
            {
                new OrderDto()
                {
                    Id="2de578aa-d36d-41c3-8061-2dc64a8f765a",
                    CustomerId="3fa578aa-d36d-41c3-8061-2dc64a8f787c",
                    TotalPrice=2000,
                    Status="Active",
                    FullAddress = "Levent Turkey Istanbul 34",
                    OrderItems=new List<OrderItemDto>
                    {
                        new OrderItemDto
                        {
                            ProductId="7723714D-BE34-438A-9F9E-57463D94DD5B",
                            Price=800,
                            ProductName="Iphone 14 Plus",
                            Quantity=1
                        },
                        new OrderItemDto
                        {
                            ProductId="46A02782-F572-4C86-860E-8F908FC105CE",
                            Price=1200,
                            ProductName="Asus Zenbook",
                            Quantity=1
                        }
                    }
                },
                new OrderDto()
                {
                    Id="3ad578aa-d36d-41c3-8061-2dc64a8f754f",
                    CustomerId="3fa578aa-d36d-41c3-8061-2dc64a8f787c",
                    TotalPrice=1600,
                    Status="Active",
                    FullAddress = "Merkez Turkey Isparta 32",
                    OrderItems=new List<OrderItemDto>
                    {
                        new OrderItemDto
                        {
                            ProductId="7723714D-BE34-438A-9F9E-57463D94DD5B",
                            Price=800,
                            ProductName="Iphone 14 Plus",
                            Quantity=2
                        }
                    }
                }
            };
        }

        [Fact]
        public async Task GetAll_ActionExecute_ReturnSuccessResult()
        {
            _mockOrderService.Setup(x => x.GetAllAsync()).ReturnsAsync(_orders);

            var result = await _ordersController.GetAll();

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnOrders = Assert.IsAssignableFrom<CustomResponseDto<List<OrderDto>>>(createActionResult.Value);

            Assert.Equal<int>(2, returnOrders.Data.Count);

            //Metotların sadece 1 kez kullanılıp kullanılmadığını check ediyoruz.
            _mockOrderService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Theory]
        [InlineData("2de578aa-d36d-41c3-8061-2dc64a8f765a")]
        [InlineData("3ad578aa-d36d-41c3-8061-2dc64a8f754f")]
        public async Task GetById_ActionExecute_ReturnSuccessResult(string id)
        {
            var orderDto = _orders.First(x => x.Id == id);

            //OrderService'i taklit ettik.
            _mockOrderService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(orderDto);

            var result = await _ordersController.GetById(id);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnOrder = Assert.IsAssignableFrom<CustomResponseDto<OrderDto>>(createActionResult.Value);

            //Metotların sadece 1 kez kullanılıp kullanılmadığını check ediyoruz.
            _mockOrderService.Verify(x => x.GetByIdAsync(id), Times.Once);
            Assert.Equal(id, returnOrder.Data.Id);
            Assert.Equal(orderDto.CustomerId, returnOrder.Data.CustomerId);
            Assert.Equal(orderDto.TotalPrice, returnOrder.Data.TotalPrice);
            Assert.Equal(orderDto.Status, returnOrder.Data.Status);
            Assert.Equal(orderDto.OrderItems, returnOrder.Data.OrderItems);
        }

        [Theory]
        [InlineData("470ef3fc-e45b-4857-9950-2ff2a8e4213z")] //Invalid Id
        public async Task GetById_IdNotFound_ReturnNotFoundException(string id)
        {
            _mockOrderService.Setup(x => x.GetByIdAsync(id)).Throws(new NotFoundException($"Order with id ({id}) didn't find in the database."));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _ordersController.GetById(id));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockOrderService.Verify(x => x.GetByIdAsync(id), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Order with id ({id}) didn't find in the database.", exception.Message);
        }

        [Theory]
        [InlineData("3fa578aa-d36d-41c3-8061-2dc64a8f787c")]
        public async Task GetByCustomerId_ActionExecute_ReturnSuccessResult(string customerId)
        {
            var orderDto = _orders.FindAll(x => x.CustomerId == customerId);

            //OrderService'i taklit ettik.
            _mockOrderService.Setup(x => x.GetByCustomerIdAsync(customerId)).ReturnsAsync(orderDto);

            var result = await _ordersController.GetByCustomerId(customerId);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, createActionResult.StatusCode);

            var returnOrder = Assert.IsAssignableFrom<CustomResponseDto<List<OrderDto>>>(createActionResult.Value);

            //Metotların sadece 1 kez kullanılıp kullanılmadığını check ediyoruz.
            _mockOrderService.Verify(x => x.GetByCustomerIdAsync(customerId), Times.Once);

            Assert.Equal<int>(orderDto.Count, returnOrder.Data.Count);
        }

        [Theory]
        [InlineData("470ef3fc-e45b-4857-9950-2ff2a8e4213z")] //Invalid Id
        public async Task GetByCustomerId_CustomerIdNotFound_ReturnNotFoundException(string customerId)
        {
            _mockOrderService.Setup(x => x.GetByCustomerIdAsync(customerId)).Throws(new NotFoundException($"Order with customerId ({customerId}) didn't find in the database."));

            Exception exception = await Assert.ThrowsAsync<NotFoundException>(() => _ordersController.GetByCustomerId(customerId));

            //Metotun çalışıp çalışmadığını test etmek için
            _mockOrderService.Verify(x => x.GetByCustomerIdAsync(customerId), Times.Once);

            Assert.IsType<NotFoundException>(exception);

            Assert.Equal($"Order with customerId ({customerId}) didn't find in the database.", exception.Message);
        }

        [Fact]
        public async Task Create_ActionExecute_ReturnSuccessResult()
        {
            var orderCreateDto = new OrderCreateDto()
            {
                CustomerId = "4va578aa-d36d-41c3-8061-2dc64a8f787z",
                TotalPrice = 2400,
                Status = "Active",
                Address = new AddressDto
                {
                    AddressLine = "Merkez",
                    City = "Isparta",
                    Country = "Türkiye",
                    CityCode = 32
                },
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto
                    {
                         ProductId="7723714D-BE34-438A-9F9E-57463D94DD5B",
                         Price=800,
                         ProductName="Iphone 14 Plus",
                         Quantity=3
                    }
                }
            };

            var orderDto = new OrderDto()
            {
                Id = "6zd578aa-d36d-41c3-8061-2dc64a8f754g",
                CustomerId = "4va578aa-d36d-41c3-8061-2dc64a8f787z",
                TotalPrice = 2400,
                Status = "Active",
                FullAddress = "Merkez Turkey Isparta 32",
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto
                    {
                         ProductId="7723714D-BE34-438A-9F9E-57463D94DD5B",
                         Price=800,
                         ProductName="Iphone 14 Plus",
                         Quantity=3
                    }
                }

            };

            _mockOrderService.Setup(x => x.CreateAsync(orderCreateDto)).ReturnsAsync(orderDto);

            var result = await _ordersController.Create(orderCreateDto);

            var createActionResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(201, createActionResult.StatusCode);

            var returnOrder = Assert.IsAssignableFrom<CustomResponseDto<OrderDto>>(createActionResult.Value);

            //Metotun çalışıp çalışmadığını test etmek için
            _mockOrderService.Verify(x => x.CreateAsync(orderCreateDto), Times.Once);

            Assert.Equal(orderCreateDto.CustomerId, returnOrder.Data.CustomerId);
            Assert.Equal(orderCreateDto.TotalPrice, returnOrder.Data.TotalPrice);
            Assert.Equal(orderCreateDto.Status, returnOrder.Data.Status);
            Assert.Equal(orderCreateDto.OrderItems.Count, returnOrder.Data.OrderItems.Count);
        }

    }
}
