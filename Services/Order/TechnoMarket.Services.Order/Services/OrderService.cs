using AutoMapper;
using MongoDB.Driver;
using System.Runtime.InteropServices;
using TechnoMarket.Services.Order.Data.Interfaces;
using TechnoMarket.Services.Order.Dtos;
using TechnoMarket.Services.Order.Models;
using TechnoMarket.Services.Order.Services.Interfaces;

namespace TechnoMarket.Services.Order.Services
{
    public class OrderService:IOrderService
    {
        //Loglama controller tarafında yapıldı.
        private readonly IOrderContext _context;
        private readonly IMapper _mapper;

        public OrderService(IOrderContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await _context.Orders.Find(o => true).ToListAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetByIdAsync(string id)
        {
            var order= await _context.Orders.Find(x => x.Id == id).SingleOrDefaultAsync();
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDto>> GetByCustomerIdAsync(string customerId)
        {
            var orders = await _context.Orders.Find(x => x.CustomerId == customerId).ToListAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> CreateAsync(OrderCreateDto orderCreateDto)
        {
            var order=_mapper.Map<Models.Order>(orderCreateDto);
            order.CreatedAt = DateTime.Now;
            await _context.Orders.InsertOneAsync(order);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> UpdateAsync(OrderUpdateDto orderUpdateDto,string id)
        {
            var order = _mapper.Map<Models.Order>(orderUpdateDto);
            order.UpdatedAt= DateTime.Now;
            //TODO: Yöntemi beğenmedim.
            order.CreatedAt = _context.Orders.Find(x => x.Id == id).SingleOrDefault().CreatedAt;
            order.Id = id;
            await _context.Orders.FindOneAndReplaceAsync(x=> x.Id== order.Id, order);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Orders.DeleteOneAsync(x=> x.Id== id);
        }


    }
}
