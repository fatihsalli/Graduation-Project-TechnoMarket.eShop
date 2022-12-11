namespace TechnoMarket.Services.Customer.Dtos
{
    public class CustomerUpdateDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }

    }
}
