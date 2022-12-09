using System.Linq.Expressions;
using TechnoMarket.Services.Customer.Dtos;

namespace TechnoMarket.Services.Customer.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(string id);
        Task<bool> AnyAsync(Expression<Func<Models.Customer, bool>> expression);
        Task<CustomerDto> AddAsync(CustomerCreateDto customerCreateDto);

    }
}
