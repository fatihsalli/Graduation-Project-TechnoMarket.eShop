namespace TechnoMarket.Services.Basket.Dtos
{
    public class BasketItemVM
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
