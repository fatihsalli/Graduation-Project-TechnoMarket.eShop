using TechnoMarket.Services.Basket.Services.Interfaces;

namespace TechnoMarket.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }





    }
}
