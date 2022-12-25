using TechnoMarket.Web.Models.Customer;
using TechnoMarket.Web.Models.Order;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerVM>> GetAllCustomersAsync();
        Task<CustomerVM> CreateOrder(CheckoutInput checkoutInput);
    }
}
