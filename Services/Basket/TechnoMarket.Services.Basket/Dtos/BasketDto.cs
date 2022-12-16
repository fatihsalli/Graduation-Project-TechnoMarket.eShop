namespace TechnoMarket.Services.Basket.Dtos
{
    public class BasketDto
    {
        public string CustomerId { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public decimal TotalPrice
        {
            get => BasketItems.Sum(x => x.Price * x.Quantity);
        }
    }
}
