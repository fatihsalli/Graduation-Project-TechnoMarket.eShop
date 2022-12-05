namespace TechnoMarket.Services.Order.Settings.Interfaces
{
    //Options pattern için
    public interface IOrderDatabaseSettings
    {
        public string OrderCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
