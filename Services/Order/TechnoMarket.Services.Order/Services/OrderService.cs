using AutoMapper;
using MongoDB.Driver;
using TechnoMarket.Services.Order.Data.Interfaces;
using TechnoMarket.Services.Order.Dtos;
using TechnoMarket.Services.Order.Services.Interfaces;
using TechnoMarket.Shared.Exceptions;

namespace TechnoMarket.Services.Order.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderContext context, IMapper mapper, ILogger<OrderService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await _context.Orders.Find(o => true).ToListAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetByIdAsync(string id)
        {
            var order = await _context.Orders.Find(x => x.Id == new Guid(id)).SingleOrDefaultAsync();

            if (order==null)
            {
                _logger.LogError($"Order with id ({id}) didn't find in the database.");
                throw new NotFoundException($"Order with id ({id}) didn't find in the database.");
            }

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDto>> GetByCustomerIdAsync(string customerId)
        {
            var orders = await _context.Orders.Find(x => x.CustomerId == new Guid(customerId)).ToListAsync();

            if (orders.Count < 1)
            {
                _logger.LogError($"Order with customerId ({customerId}) didn't find in the database.");
                throw new NotFoundException($"Order with customerId ({customerId}) didn't find in the database.");
            }

            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> CreateAsync(OrderCreateDto orderCreateDto)
        {
            var order = _mapper.Map<Models.Order>(orderCreateDto);
            order.CreatedAt = DateTime.Now;
            await _context.Orders.InsertOneAsync(order);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> UpdateAsync(OrderUpdateDto orderUpdateDto)
        {
            var orderCheck = await _context.Orders.Find(x => x.Id == new Guid(orderUpdateDto.Id)).SingleOrDefaultAsync();

            if (orderCheck == null)
            {
                _logger.LogError($"Order with id ({orderUpdateDto.Id}) didn't find in the database.");
                throw new NotFoundException($"Order with id ({orderUpdateDto.Id}) didn't find in the database.");
            }

            var order = _mapper.Map<Models.Order>(orderUpdateDto);
            order.UpdatedAt = DateTime.Now;
            //TODO: Yöntemi beğenmedim.
            order.CreatedAt = orderCheck.CreatedAt;
            await _context.Orders.FindOneAndReplaceAsync(x => x.Id == order.Id, order);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task DeleteAsync(string id)
        {
            var order = await _context.Orders.Find(x => x.Id == new Guid(id)).SingleOrDefaultAsync();

            if (order == null)
            {
                _logger.LogError($"Order with id ({id}) didn't find in the database.");
                throw new NotFoundException($"Order with id ({id}) didn't find in the database.");
            }

            await _context.Orders.DeleteOneAsync(x => x.Id == new Guid(id));
        }

        public async Task ChangeStatusAsync(OrderStatusUpdateDto orderStatusUpdateDto)
        {
            var order = await _context.Orders.Find(x => x.Id == new Guid(orderStatusUpdateDto.Id)).SingleOrDefaultAsync();

            if (order == null)
            {
                _logger.LogError($"Order with id ({order.Id}) didn't find in the database.");
                throw new NotFoundException($"Order with id ({order.Id}) didn't find in the database.");
            }

            order.Status = orderStatusUpdateDto.Status;
            await _context.Orders.FindOneAndReplaceAsync(x => x.Id == order.Id, order);
        }
    }
}
