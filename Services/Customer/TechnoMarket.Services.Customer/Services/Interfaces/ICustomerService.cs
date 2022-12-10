using System.Linq.Expressions;
using TechnoMarket.Services.Customer.Dtos;

namespace TechnoMarket.Services.Customer.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync();
        Task<List<CustomerDtoWithAddress>> GetCustomersWithAddressAsync();
        Task<CustomerDto> GetByIdAsync(string id);
        Task<CustomerDtoWithAddress> GetByIdWithAddressAsync(string id);
        Task<CustomerDtoWithAddress> AddAsync(CustomerCreateDto customerCreateDto);
        Task UpdateAsync(CustomerUpdateDto customerUpdateDto);
        Task RemoveAsync(string id);
        IQueryable<Models.Customer> Where(Expression<Func<Models.Customer, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<Models.Customer, bool>> expression);
    }
}
