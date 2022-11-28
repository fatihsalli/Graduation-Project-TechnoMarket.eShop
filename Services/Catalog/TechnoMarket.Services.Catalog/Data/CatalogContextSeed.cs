using MongoDB.Driver;
using System.Runtime.CompilerServices;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Services.Interfaces;

namespace TechnoMarket.Services.Catalog.Data
{
    public class CatalogContextSeed
    {
        //TODO: Category program cs tarafında kaydedip sonra product oluşturmak istiyorum. CategoryId lazım çünkü. İlk açıldığında bulamıyor caregory'i.
        public static void SeedData(IMongoCollection<Product> productCollection, IMongoCollection<Category> categoryCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();

            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts(categoryCollection));
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts(IMongoCollection<Category> categoryCollection)
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name="Iphone X",
                    Stock=10,
                    Price=950.00M,
                    Description="Super Retina HD display and A11 Bionic chip with 64-bit architecture",
                    CreatedAt=DateTime.Now,
                    Feature=new ProductFeature{Color="black",Height="5.65 inches",Width="2.79 inches",Weight=" 6.14 ounces"},
                    Category=categoryCollection.Find(x=> x.Name=="Smart Phone").SingleOrDefault()
                }
            };
        }
    }
}
