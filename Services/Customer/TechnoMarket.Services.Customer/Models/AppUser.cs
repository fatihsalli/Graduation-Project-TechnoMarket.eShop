using Microsoft.AspNetCore.Identity;

namespace TechnoMarket.Services.Customer.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
