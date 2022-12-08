using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoMarket.Services.Customer.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public Address Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
