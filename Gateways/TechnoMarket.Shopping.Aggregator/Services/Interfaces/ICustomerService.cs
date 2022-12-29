using TechnoMarket.Shopping.Aggregator.Models.Customer;

namespace TechnoMarket.Shopping.Aggregator.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerModel> GetCustomer(string email);
        Task<CustomerModel> AddAsync(CustomerCreateModel customerCreateModel);
    }
}
