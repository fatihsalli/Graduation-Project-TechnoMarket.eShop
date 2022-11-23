namespace TechnoMarket.Services.Catalog.Settings
{
    //Options pattern için
    public class CatalogDatabaseSettings : ICatalogDatabaseSettings
    {
        public string ProductCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
