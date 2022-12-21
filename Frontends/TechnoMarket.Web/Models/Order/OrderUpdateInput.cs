namespace TechnoMarket.Services.Order.Dtos
{
    public class OrderUpdateInput
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public AddressVM Address { get; set; }
        public List<OrderItemVM> OrderItems { get; set; }
    }
}
