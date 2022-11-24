namespace TechnoMarket.Services.Catalog.Settings.Interfaces
{
    //Options pattern için
    public interface ICatalogDatabaseSettings
    {
        public string ProductCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}
