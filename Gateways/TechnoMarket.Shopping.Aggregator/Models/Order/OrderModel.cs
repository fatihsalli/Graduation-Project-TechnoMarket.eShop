namespace TechnoMarket.Shopping.Aggregator.Models.Order
{
    public class OrderModel
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string FullAddress { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
