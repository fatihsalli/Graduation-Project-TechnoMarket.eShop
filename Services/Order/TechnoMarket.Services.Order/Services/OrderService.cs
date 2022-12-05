using AutoMapper;
using MongoDB.Driver;
using System.Runtime.InteropServices;
using TechnoMarket.Services.Order.Data.Interfaces;
using TechnoMarket.Services.Order.Models;
using TechnoMarket.Services.Order.Services.Interfaces;

namespace TechnoMarket.Services.Order.Services
{
    public class OrderService:IOrderService
    {
        //Mapleme ve loglama controller tarafında yapıldı.
        private readonly IOrderContext _context;

        public OrderService(IOrderContext context)
        {
            _context= context ??throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Models.Order>> GetAllAsync()
        {
            return await _context.Orders.Find(o => true).ToListAsync();
        }

        public async Task<Models.Order> GetByIdAsync(string id)
        {
            return await _context.Orders.Find(x=> x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<List<Models.Order>> GetByCustomerIdAsync(string customerId)
        {
            return await _context.Orders.Find(x=> x.CustomerId== customerId).ToListAsync();
        }

        public async Task<Models.Order> CreateAsync(Models.Order order)
        {
            order.CreatedAt=DateTime.Now;
            await _context.Orders.InsertOneAsync(order);
            return order;
        }

        public async Task<Models.Order> UpdateAsync(Models.Order order)
        {
            var result= await _context.Orders.FindOneAndReplaceAsync(x=> x.Id== order.Id, order);
            return result;
        }

        public async Task<bool> Delete(string id)
        {
            var result=await _context.Orders.DeleteOneAsync(x=> x.Id== id);

            if (result.DeletedCount<1)
            {
                return false;
            }

            return true;
        }


    }
}
