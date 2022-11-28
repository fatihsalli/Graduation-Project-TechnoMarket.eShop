using MongoDB.Driver;
using System.Runtime.CompilerServices;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Services.Interfaces;

namespace TechnoMarket.Services.Catalog.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection, IMongoCollection<Category> categoryCollection)
        {
            bool existCategories = categoryCollection.Find(p => true).Any();

            if (!existCategories)
            {
                categoryCollection.InsertMany(GetPreConfiguredCategories());
            }

            bool existProducts= productCollection.Find(p => true).Any();

            if (!existProducts)
            {
                productCollection.InsertMany(GetPreconfiguredProducts(categoryCollection));
            }
        }

        private static IEnumerable<Category> GetPreConfiguredCategories()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Name = "Notebook"
                },
                new Category()
                {
                    Name = "Smart Phone"
                }
            };
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
                    Feature=new ProductFeature{Color="black",Height="5.65 inches",Width="2.79 inches",Weight="0.6 kg"},
                    Category=categoryCollection.Find(x=> x.Name=="Smart Phone").SingleOrDefault()
                },
                new Product()
                {
                    Name="Asus Zenbook Pro Duo 15",
                    Stock=6,
                    Price=3500.00M,
                    Description="ZenBook Pro Duo 15 OLED lets you get things done in style: calmly, efficiently, and with zero fuss. It’s your powerful and elegant next-level companion for on-the-go productivity and creativity, featuring an amazing 4K OLED HDR touchscreen.",
                    CreatedAt=DateTime.Now,
                    Feature=new ProductFeature{Color="black",Height="9.81 inches",Width="14.17 inches",Weight="2.34 kg"},
                    Category=categoryCollection.Find(x=> x.Name=="Notebook").SingleOrDefault()
                }
            };
        }
    }
}
