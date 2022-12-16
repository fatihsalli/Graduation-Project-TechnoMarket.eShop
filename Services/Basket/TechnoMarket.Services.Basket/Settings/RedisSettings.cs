using TechnoMarket.Services.Basket.Settings.Interfaces;

namespace TechnoMarket.Services.Basket.Settings
{
    //Options Pattern
    public class RedisSettings : IRedisSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
