using Microsoft.AspNetCore.Identity;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Web.Models;
using TechnoMarket.Web.Models.Customer;
using TechnoMarket.Web.Models.Order;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerService(HttpClient httpClient, UserManager<IdentityUser> userManager)
        {
            _httpClient = httpClient;
            _userManager = userManager;
        }

        //=> For Customer
        public async Task<List<CustomerVM>> GetAllCustomersAsync()
        {
            var response = await _httpClient.GetAsync("customers");

            if (!response.IsSuccessStatusCode)
            {
                //Exception-loglama
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<CustomerVM>>>();
            return responseSuccess.Data;
        }

        public async Task<CustomerVM> GetCustomerByEmailAsync(string email)
        {
            var response = await _httpClient.GetAsync($"customers/GetCustomerByEmail/{email}");

            if (!response.IsSuccessStatusCode)
            {
                //Exception-loglama
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<CustomResponseDto<CustomerVM>>();
            return responseSuccess.Data;
        }

        public async Task<CustomerVM> CreateOrder(CheckoutInput checkoutInput)
        {
            var user = _userManager.Users.FirstOrDefault();

            var customerCreateInput = new CustomerCreateInput()
            {
                FirstName = checkoutInput.FirstName,
                LastName = checkoutInput.LastName,
                Email = user.Email,
                Address = new AddressVM
                {
                    AddressLine = checkoutInput.AddressLine,
                    City = checkoutInput.City,
                    CityCode = checkoutInput.CityCode,
                    Country = checkoutInput.Country
                }
            };

            var response = await _httpClient.PostAsJsonAsync<CustomerCreateInput>("customers", customerCreateInput);

            var responseSuccess = await response.Content.ReadFromJsonAsync<CustomResponseDto<CustomerVM>>();

            return responseSuccess.Data;
        }




    }
}
