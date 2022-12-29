using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Services.Basket.Dtos;
using TechnoMarket.Services.Basket.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Shared.Messages;
using static StackExchange.Redis.Role;

namespace TechnoMarket.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public BasketsController(IBasketService basketService, ISendEndpointProvider sendEndpointProvider)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _sendEndpointProvider = sendEndpointProvider ?? throw new ArgumentNullException(nameof(sendEndpointProvider));
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomResponseDto<BasketDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBasket(string userId)
        {
            var basketDto = await _basketService.GetBasket(userId);
            return CreateActionResult(CustomResponseDto<BasketDto>.Success(200, basketDto));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SaveOrUpdateBasket([FromBody] BasketDto basketDto)
        {
            await _basketService.SaveOrUpdate(basketDto);
            return CreateActionResult(CustomResponseDto<bool>.Success(204));
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteBasket(string userId)
        {
            await _basketService.Delete(userId);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        /// <summary>
        /// MassTransit ile birlikte order oluşturulabilmesi için Command gönderiyoruz. Asenkron iletişim olması için yapılmıştır. Direkt olarak controller tarafında yazıldı.
        /// </summary>
        /// <param name="basketCheckOutDto"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> CheckOut([FromBody] BasketCheckOutDto basketCheckOutDto)
        {
            //Kuyruk oluşturduk
            var sendEndPoint =await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand()
            {
                CustomerId=basketCheckOutDto.CustomerId,
                Status= basketCheckOutDto.Status,
                TotalPrice=basketCheckOutDto.TotalPrice,
                Address=new Shared.Messages.AddressDto
                {
                    AddressLine=basketCheckOutDto.Address.AddressLine,
                    City = basketCheckOutDto.Address.City,
                    Country = basketCheckOutDto.Address.Country,
                    CityCode = basketCheckOutDto.Address.CityCode,
                }
            };

            basketCheckOutDto.OrderItems.ForEach(x =>
            {
                createOrderMessageCommand.OrderItems.Add(new Shared.Messages.OrderItemDto
                {
                    Quantity = x.Quantity,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName
                });
            });

            //Mesajı gönderiyoruz. Order ayakta olmasa bile mesaj kuyrukta bekleyecek.
            await sendEndPoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
