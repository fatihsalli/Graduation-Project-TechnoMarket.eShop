using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Services.Basket.Dtos;
using TechnoMarket.Services.Basket.Services;
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
        public async Task<IActionResult> GetBasket(string customerId)
        {
            var basketDto=await _basketService.GetBasket(customerId);
            return CreateActionResult(CustomResponseDto<BasketDto>.Success(200, basketDto));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {
            var response = await _basketService.SaveOrUpdate(basketDto);
            return CreateActionResult(CustomResponseDto<bool>.Success(204));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string customerId)
        {
            var response=await _basketService.Delete(customerId);
            return CreateActionResult(CustomResponseDto<bool>.Success(204));
        }

    }
}
