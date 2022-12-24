using Microsoft.AspNetCore.Identity;

namespace TechnoMarket.AuthServer.Models
{
    public class UserApp : IdentityUser
    {
        public string City { get; set; }
    }
}
