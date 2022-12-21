using TechnoMarket.Services.Customer.Dtos;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerVM>> GetAllCustomersAsync();
    }
}
