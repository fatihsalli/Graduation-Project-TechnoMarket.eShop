namespace TechnoMarket.Shopping.Aggregator.Models.Customer
{
    public class CustomerCreateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }
    }
}
