using AutoMapper;
using MassTransit;
using TechnoMarket.Services.Order.Dtos;
using TechnoMarket.Services.Order.Services.Interfaces;
using TechnoMarket.Shared.Messages;

namespace TechnoMarket.Services.Order.Consumers
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        private readonly IOrderService _orderService;

        public CreateOrderMessageCommandConsumer(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var message = context.Message;

            var orderCreateDto=new OrderCreateDto()
            { 
                CustomerId= message.CustomerId,
                TotalPrice= message.TotalPrice,
                Address=new Dtos.AddressDto() 
                {
                    AddressLine=message.Address.AddressLine,
                    City = message.Address.City,
                    Country = message.Address.Country,
                    CityCode = message.Address.CityCode,
                }
            };

            message.OrderItems.ForEach(x =>
            {
                orderCreateDto.OrderItems.Add(new Dtos.OrderItemDto
                {
                    Quantity = x.Quantity,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName
                });
            });

            await _orderService.CreateAsync(orderCreateDto);
        }
    }
}
