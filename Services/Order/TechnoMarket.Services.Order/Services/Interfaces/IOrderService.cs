using TechnoMarket.Services.Order.Dtos;

namespace TechnoMarket.Services.Order.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<List<OrderDto>> GetAllAsync();
        public Task<OrderDto> GetByIdAsync(string id);
        public Task<List<OrderDto>> GetByCustomerIdAsync(string customerId);
        public Task<OrderDto> CreateAsync(OrderCreateDto orderCreateDto);
        public Task<OrderDto> UpdateAsync(OrderUpdateDto orderUpdateDto);
        public Task DeleteAsync(string id);
        public Task ChangeStatusAsync(OrderStatusUpdateDto orderStatusUpdateDto);

    }
}
