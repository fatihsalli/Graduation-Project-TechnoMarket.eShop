using StackExchange.Redis;

namespace TechnoMarket.Services.Basket.Services
{
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;
        private ConnectionMultiplexer _connectionMultiplexer;

        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        //Redis tarafında bağlantıyı belirttik
        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        //Veritabanı için
        public IDatabase GetDb(int db = 1) => _connectionMultiplexer.GetDatabase(db);


    }
}
