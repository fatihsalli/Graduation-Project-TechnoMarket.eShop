using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Services.Basket.Dtos;
using TechnoMarket.Services.Basket.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Shared.Messages;

namespace TechnoMarket.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IMapper _mapper;

        public BasketsController(IBasketService basketService, ISendEndpointProvider sendEndpointProvider,IMapper mapper)
        {
            _basketService = basketService;
            _sendEndpointProvider = sendEndpointProvider;
            _mapper = mapper;
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

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CheckOut([FromBody] BasketCheckOutDto basketCheckOutDto)
        {
            //Kuyruk oluşturduk
            var sendEndPoint =await _sendEndpointProvider.GetSendEndpoint(new Uri("gueue:create-order-service"));

            var createOrderMessageCommand = _mapper.Map<CreateOrderMessageCommand>(basketCheckOutDto);

            await sendEndPoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
