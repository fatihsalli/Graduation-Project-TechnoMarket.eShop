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
            _redisService = redisService ?? throw new ArgumentNullException(nameof(redisService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Delete(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);

            if (!status)
            {
                _logger.LogError($"Basket with id ({userId}) didn't find in the database.");
                throw new NotFoundException($"Basket with id ({userId}) didn't find in the database.");
            }
        }

        public async Task<BasketDto> GetBasket(string userId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId);

            if (existBasket.IsNullOrEmpty)
            {
                _logger.LogError($"Basket with id ({userId}) didn't find in the database.");
                throw new NotFoundException($"Basket with id ({userId}) didn't find in the database.");
            }
            return JsonSerializer.Deserialize<BasketDto>(existBasket);
        }

        public async Task SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

            if (!status)
            {
                _logger.LogError($"Basket didn't save or update in the database.");
                throw new Exception($"Basket didn't save or update in the database.");
            }

        }
    }
}
