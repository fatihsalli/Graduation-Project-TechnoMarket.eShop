using TechnoMarket.Web.Models;

namespace TechnoMarket.Web.Models.Customer
{
    public class CustomerCreateInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AddressVM Address { get; set; }

    }
}
