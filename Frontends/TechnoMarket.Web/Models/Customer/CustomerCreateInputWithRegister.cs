using TechnoMarket.Web.Models;

namespace TechnoMarket.Services.Customer.Dtos
{
    public class CustomerCreateInputWithRegister
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public AddressVM Address { get; set; }

    }
}
