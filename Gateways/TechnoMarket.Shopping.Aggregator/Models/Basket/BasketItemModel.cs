namespace TechnoMarket.Shopping.Aggregator.Models.Basket
{
    public class BasketItemModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
