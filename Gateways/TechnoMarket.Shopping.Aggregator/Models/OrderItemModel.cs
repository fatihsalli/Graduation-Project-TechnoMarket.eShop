namespace TechnoMarket.Shopping.Aggregator.Models
{
    public class OrderItemModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
