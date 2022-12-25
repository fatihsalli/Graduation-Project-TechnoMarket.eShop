using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Services.Basket.Dtos;
using TechnoMarket.Services.Basket.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
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

    }
}
