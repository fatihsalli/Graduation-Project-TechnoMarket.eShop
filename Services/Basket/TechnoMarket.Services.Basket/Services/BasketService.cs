using System.Text.Json;
using TechnoMarket.Services.Basket.Dtos;
using TechnoMarket.Services.Basket.Services.Interfaces;
using TechnoMarket.Shared.Exceptions;

namespace TechnoMarket.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;
        private readonly ILogger<BasketService> _logger;
        public BasketService(RedisService redisService, ILogger<BasketService> logger)
        {
            _redisService = redisService;
            _logger = logger;
        }

        public async Task Delete(string customerId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(customerId);

            if (!status)
            {
                _logger.LogError($"Basket with id ({customerId}) didn't find in the database.");
                throw new NotFoundException($"Basket with id ({customerId}) didn't find in the database.");
            }
        }

        public async Task<BasketDto> GetBasket(string customerId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(customerId);

            if (existBasket.IsNullOrEmpty)
            {
                _logger.LogError($"Basket with id ({customerId}) didn't find in the database.");
                throw new NotFoundException($"Basket with id ({customerId}) didn't find in the database.");
            }
            return JsonSerializer.Deserialize<BasketDto>(existBasket);
        }

        public async Task SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.CustomerId, JsonSerializer.Serialize(basketDto));

            if (!status)
            {
                _logger.LogError($"Basket didn't save or update in the database.");
                throw new Exception($"Basket didn't save or update in the database.");
            }

        }
    }
}
