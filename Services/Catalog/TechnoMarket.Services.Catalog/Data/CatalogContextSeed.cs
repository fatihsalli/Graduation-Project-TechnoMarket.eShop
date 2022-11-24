using MongoDB.Driver;
using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();

            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name="Asus Notebook i7",
                    Price=20000.00M,
                    ImageFile="product-1.jpeg",
                    CustomerId="bbb3a5ba-1288-4537-a868-e95a0d162139",
                    CreatedAt=DateTime.Now,
                    Description="16 gb ram i7 processor 17.3'inch",
                    Feature=new ProductFeature() {Color="black",Summary="last technology of notebook"},
                    Category=new Category(){Name="Notebook"}                  
                }
            };
        }
    }
}
