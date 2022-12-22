namespace TechnoMarket.Services.Basket.Dtos
{
    public class BasketVM
    {
        public string CustomerId { get; set; }
        public List<BasketItemVM> BasketItems { get; set; }
        public decimal TotalPrice
        {
            get => BasketItems.Sum(x => x.Price * x.Quantity);
        }
    }
}
