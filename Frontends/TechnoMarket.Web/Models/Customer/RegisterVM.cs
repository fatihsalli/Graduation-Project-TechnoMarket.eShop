using System.ComponentModel.DataAnnotations;

namespace TechnoMarket.Services.Customer.Dtos
{
    public class RegisterVM
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string CustomerId { get; set; }

    }
}
