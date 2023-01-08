using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using TechnoMarket.Services.Order.Data.Interfaces;
using TechnoMarket.Services.Order.Models;
using TechnoMarket.Services.Order.Services;
using TechnoMarket.Services.Order.Services.Interfaces;
using TechnoMarket.Shared.Exceptions;
using Xunit;
using OrderEntity = TechnoMarket.Services.Order.Models.Order;

namespace TechnoMarket.Services.Catalog.UnitTests
{
    public class OrderServiceTest
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<OrderService>> _mockLogger;
        private readonly Mock<IOrderContext> _mockContext;
        private readonly IOrderService _orderService;
        private List<OrderEntity> _orders;

        public OrderServiceTest()
        {
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<OrderService>>();
            _mockContext = new Mock<IOrderContext>();
            _orderService = new OrderService(_mockContext.Object,_mockMapper.Object,_mockLogger.Object);

            _orders = new List<OrderEntity>()
            {
                new OrderEntity
                {
                    CustomerId=new Guid("3fa578aa-d36d-41c3-8061-2dc64a8f787c"),
                    TotalPrice=2000,
                    Status="Active",
                    CreatedAt=DateTime.Now,
                    OrderItems=new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId=new Guid("7723714D-BE34-438A-9F9E-57463D94DD5B"),
                            Price=800,
                            ProductName="Iphone 14 Plus",
                            Quantity=1
                        },
                        new OrderItem
                        {
                            ProductId=new Guid("46A02782-F572-4C86-860E-8F908FC105CE"),
                            Price=1200,
                            ProductName="Asus Zenbook",
                            Quantity=1
                        }
                    },
                    Address = new Address
                    {
                        AddressLine = "Levent",
                        City = "Istanbul",
                        CityCode = 34,
                        Country = "Turkey"
                    }
                }
            };

        }


    }
}

