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
                //ordercollection.InsertMany(GetPreConfiguredOrders());
            }
        }

        //private static IEnumerable<Models.Order> GetPreConfiguredOrders()
        //{
        //    return new List<Models.Order>
        //    {
        //        new Models.Order
        //        {
        //            CustomerId="e14d43229930f9f6f56e1dc2",
        //            Quantity=10,
        //            Price=15000,
        //            Status="Active",
        //            CreatedAt=DateTime.Now,
        //            Product=new Product{Id="d980f42bc94c0f1ce78194a8",ImageUrl="iphonex_smartphone.jpg",Name="Iphone X"},
        //            Address=new Address{AddressLine="Levent",City="Istanbul",CityCode=34,Country="Turkey"}
        //        },
        //        new Models.Order
        //        {
        //            CustomerId="d22f651b720c26c40643175a",
        //            Quantity=8,
        //            Price=25000,
        //            Status="Active",
        //            CreatedAt=DateTime.Now,
        //            Product=new Product{Id="912ce512f0091e334eb4bfbe",ImageUrl="asus_zenbook_notebook.jpg",Name="Asus Zenbook Pro Duo 15"},
        //            Address=new Address{AddressLine="Sarıyer",City="Istanbul",CityCode=34,Country="Turkey"}
        //        }
        //    };
        //}
    }
}
