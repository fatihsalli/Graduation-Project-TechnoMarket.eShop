using MongoDB.Driver;
using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection,IMongoCollection<Category> categoryCollection)
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
                    Id="398ca86b-de80-4687-81c0-c97a4302bf51",
                    Name="Asus Notebook i7",
                    Price=20000.00M,
                    ImageFile="product-1.jpeg",
                    CustomerId="bbb3a5ba-1288-4537-a868-e95a0d162139",
                    CreatedAt=DateTime.Now,
                    Description="16 gb ram i7 processor 17.3'inch",
                    Feature=new Feature() {Color="black",Summary="last technology of notebook"},
                    CategoryId="8da751b2-c299-4914-a9a4-03968614e157",
                    Category=new Category(){Id="8da751b2-c299-4914-a9a4-03968614e157",Name="Notebook"}                  
                }
            };
        }
    }
}
