namespace TechnoMarket.Services.Customer.Dtos
{
    public class CustomerUpdateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }

    }
}
