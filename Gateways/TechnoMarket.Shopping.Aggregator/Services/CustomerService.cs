using System.Net.Http;
using TechnoMarket.Shopping.Aggregator.Extensions;
using TechnoMarket.Shopping.Aggregator.Models.Customer;
using TechnoMarket.Shopping.Aggregator.Services.Interfaces;

namespace TechnoMarket.Shopping.Aggregator.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<CustomerModel> AddAsync(CustomerCreateModel customerCreateModel)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/customers",customerCreateModel);
            return await response.ReadContentAs<CustomerModel>();
        }

        public async Task<CustomerModel> GetCustomer(string email)
        {
            var response = await _httpClient.GetAsync($"/api/customers/GetCustomerByEmail/{email}");
            return await response.ReadContentAs<CustomerModel>();
        }
    }
}
