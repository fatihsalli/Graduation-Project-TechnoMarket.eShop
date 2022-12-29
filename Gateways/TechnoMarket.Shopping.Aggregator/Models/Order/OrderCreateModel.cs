namespace TechnoMarket.Shopping.Aggregator.Models.Order
{
    public class OrderCreateModel
    {
        public OrderCreateModel()
        {
            OrderItems = new List<OrderItemModel>();
        }

        public string CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public AddressModel Address { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
