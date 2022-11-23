using MongoDB.Driver;
using TechnoMarket.Services.Catalog.Data.Interfaces;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Settings;

namespace TechnoMarket.Services.Catalog.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(ICatalogDatabaseSettings catalogDatabaseSettings)
        {
            var client=new MongoClient(catalogDatabaseSettings.ConnectionString);

            var database = client.GetDatabase(catalogDatabaseSettings.DatabaseName);

            Products = database.GetCollection<Product>(catalogDatabaseSettings.ProductCollectionName);
            Categories=database.GetCollection<Category>(catalogDatabaseSettings.CategoryCollectionName);
        }

        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<Category> Categories { get; }
    }
}
