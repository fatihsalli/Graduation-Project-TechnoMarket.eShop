namespace TechnoMarket.Services.Customer.Repositories.Interfaces
{
    public interface ICustomerRepository:IGenericRepository<Models.Customer>
    {
        Task<List<Models.Customer>> GetCustomersWithAddressAsync();
        Task<Models.Customer> GetSingleCustomerByIdWithAddressAsync(string customerId);

    }
}
