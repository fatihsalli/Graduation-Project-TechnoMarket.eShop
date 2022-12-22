using System.ComponentModel.DataAnnotations;

namespace TechnoMarket.Services.Customer.Dtos
{
    public class LoginInput
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
