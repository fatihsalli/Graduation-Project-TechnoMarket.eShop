namespace TechnoMarket.Web.Models.Customer
{
    public class CustomerUpdateInput
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AddressVM Address { get; set; }

    }
}
