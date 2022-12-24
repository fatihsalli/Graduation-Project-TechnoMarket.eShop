using TechnoMarket.Shared.Dtos;
using TechnoMarket.Web.Models.Customer;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;

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

        //=> For Register
        public async Task<bool> RegisterCustomer(CustomerCreateInputWithRegister customer)
        {
            var customerCreateInput = new CustomerCreateInput()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = customer.Address
            };
            var responseCustomer = await _httpClient.PostAsJsonAsync<CustomerCreateInput>("customers", customerCreateInput);

            var responseSuccess = await responseCustomer.Content.ReadFromJsonAsync<CustomResponseDto<CustomerVM>>();

            var registerVM = new RegisterVM()
            {
                Username = customer.Username,
                Email = customer.Email,
                Password = customer.Password,
                CustomerId = responseSuccess.Data.Id
            };

            var responseUser = await _httpClient.PostAsJsonAsync<RegisterVM>("users/register", registerVM);

            if (!responseUser.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        //=> For Login
        public async Task<bool> LoginUser(LoginInput loginInput)
        {
            var responseUser = await _httpClient.PostAsJsonAsync<LoginInput>("users/login", loginInput);

            if (!responseUser.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public class User
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        public class UserInit
        {
            public static List<User> Users()
            {
                return new List<User>()
                {
                    new User() {UserId=1,UserName="test_username",Password="test_password",Role="test_role"},
                };
            }
        }






    }
}
