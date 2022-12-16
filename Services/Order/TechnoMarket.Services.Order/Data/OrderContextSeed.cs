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
                    TotalPrice=30000,
                    Status="Active",
                    CreatedAt=DateTime.Now,
                    OrderItems=new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId=new Guid("46a02782-f572-4c86-860e-8f908fc105ce"),
                            Price=30000,
                            ProductName="Iphone 14 Plus",
                            Quantity=1
                        }
                    },
                    Address = new Address 
                    { 
                        AddressLine = "Levent", 
                        City = "Istanbul", 
                        CityCode = 34, 
                        Country = "Turkey" 
                    }
                }
            };
        }
    }
}
