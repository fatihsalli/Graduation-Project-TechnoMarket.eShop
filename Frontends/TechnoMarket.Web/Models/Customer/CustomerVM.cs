namespace TechnoMarket.Services.Customer.Dtos
{
    public class CustomerVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AddressVM Address { get; set; }
    }
}
