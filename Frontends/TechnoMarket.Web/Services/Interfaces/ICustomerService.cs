using TechnoMarket.Web.Models.Customer;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerVM>> GetAllCustomersAsync();
    }
}
