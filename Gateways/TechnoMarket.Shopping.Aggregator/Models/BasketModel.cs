namespace TechnoMarket.Shopping.Aggregator.Models
{
    public class BasketModel
    {
        public string UserId { get; set; }
        public List<BasketItemExtendedModel> BasketItems { get; set; }
        public decimal TotalPrice
        {
            get => BasketItems.Sum(x => x.Price * x.Quantity);
        }

    }
}
