﻿using System.Text.Json;
using TechnoMarket.Services.Basket.Dtos;
using TechnoMarket.Services.Basket.Services.Interfaces;
using TechnoMarket.Shared.Exceptions;

namespace TechnoMarket.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;
        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<bool> Delete(string customerId)
        {
            var status=await _redisService.GetDb().KeyDeleteAsync(customerId);
            return status;
        }

        public async Task<BasketDto> GetBasket(string customerId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(customerId);

            if (existBasket.IsNullOrEmpty)
            {
                //Loglama
                throw new NotFoundException($"Basket with id ({customerId}) didn't find in the database.");
            }
            return JsonSerializer.Deserialize<BasketDto>(existBasket);
        }

        public async Task<bool> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.CustomerId, JsonSerializer.Serialize(basketDto));
            return status;
        }
    }
}