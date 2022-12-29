

using MassTransit;
using MongoDB.Driver;
using TechnoMarket.Services.Order.Data.Interfaces;
using TechnoMarket.Shared.Messages;

namespace TechnoMarket.Services.Order.Consumers
{
    //Catalog.Api ProductService=> Update metodu içerisinde eventı fırlatacak.
    public class ProductNameChangedEventConsumer : IConsumer<ProductNameChangedEvent>
    {
        private readonly IOrderContext _context;
        public ProductNameChangedEventConsumer(IOrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Consume(ConsumeContext<ProductNameChangedEvent> context)
        {
            var orders = await _context.Orders.Find(o => true).ToListAsync();

            orders.ForEach(o =>
            {
                o.OrderItems.ForEach(od =>
                {
                    if (od.ProductId == new Guid(context.Message.ProductId))
                    {
                        od.ProductName = context.Message.UpdatedName;
                    }
                });
            });

            foreach (var item in orders)
            {
                await _context.Orders.FindOneAndReplaceAsync(x => x.Id == item.Id, item);
            }
        }
    }
}
