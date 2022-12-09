using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoMarket.Services.Customer.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int CityCode { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
