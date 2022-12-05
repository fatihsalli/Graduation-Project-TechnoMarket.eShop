namespace TechnoMarket.Services.Order.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<List<Models.Order>> GetAllAsync();
        public Task<Models.Order> GetByIdAsync(string id);
        public Task<List<Models.Order>> GetByCustomerIdAsync(string customerId);
        public Task<Models.Order> CreateAsync(Models.Order order);
        public Task<Models.Order> UpdateAsync(Models.Order order);

    }
}
