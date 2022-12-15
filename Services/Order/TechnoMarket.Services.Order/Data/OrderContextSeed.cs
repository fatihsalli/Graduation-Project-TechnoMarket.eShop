using MongoDB.Driver;
using TechnoMarket.Services.Order.Models;

namespace TechnoMarket.Services.Order.Data
{
    public class OrderContextSeed
    {
        public static void SeedData(IMongoCollection<Models.Order> ordercollection)
        {
            bool existOrders = ordercollection.Find(order => true).Any();

            if (!existOrders)
            {
                ordercollection.InsertMany(GetPreConfiguredOrders());
            }
        }

        private static IEnumerable<Models.Order> GetPreConfiguredOrders()
        {
            return new List<Models.Order>
            {
                new Models.Order
                {
                    CustomerId=new Guid("3fa578aa-d36d-41c3-8061-2dc64a8f787c"),
                    Quantity=10,
                    Price=15000,
                    Status="Active",
                    CreatedAt=DateTime.Now,
                    Product=new Product{Id=new Guid("46a02782-f572-4c86-860e-8f908fc105ce"),ImageUrl="asuszenbook.jpeg",Name="Asus Zenbook"},
                    Address=new Address{AddressLine="Levent",City="Istanbul",CityCode=34,Country="Turkey"}
                },
                new Models.Order
                {
                    CustomerId=new Guid("3fa578aa-d36d-41c3-8061-2dc64a8f787c"),
                    Quantity=8,
                    Price=25000,
                    Status="Active",
                    CreatedAt=DateTime.Now,
                    Product=new Product{Id=new Guid("7723714d-be34-438a-9f9e-57463d94dd5b"),ImageUrl="appleiphone14.jpeg",Name="Iphone 14 Plus"},
                    Address=new Address{AddressLine="Sarıyer",City="Istanbul",CityCode=34,Country="Turkey"}
                }
            };
        }
    }
}
