namespace TechnoMarket.Web.Models.Order
{
    public class OrderCreateInput
    {
        public string CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public AddressVM Address { get; set; }
        public List<OrderItemVM> OrderItems { get; set; }

    }
}
