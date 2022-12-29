namespace TechnoMarket.Shopping.Aggregator.Models
{
    public class CheckOutModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }

    }
}
