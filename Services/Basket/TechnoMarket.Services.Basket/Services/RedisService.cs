using StackExchange.Redis;

namespace TechnoMarket.Services.Basket.Services
{
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;
        //StackExchange kütüphanesi ile gelen connection için kullanıyoruz.
        private ConnectionMultiplexer _connectionMultiplexer;

        //appsettings içerisindeki değer => program.cs tarafından gönderilecek.
        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        //Redis tarafında bağlantıyı belirttik
        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        #region Default Olarak Gelen Veritabanı Seçimi
        //Default gelen veritabanlarından birini seçiyoruz. Neden 1 den fazla default veritabanı geliyor? Bir tanesini test bir tanesini development bir tanesini production kullanmak gibi avantajlar sağlar. 
        #endregion
        public IDatabase GetDb(int db = 1) => _connectionMultiplexer.GetDatabase(db);
    }
}
