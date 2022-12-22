namespace TechnoMarket.Web.Models.Basket
{
    public class BasketItemVM
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
    }
}
