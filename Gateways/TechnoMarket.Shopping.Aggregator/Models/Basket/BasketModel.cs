namespace TechnoMarket.Shopping.Aggregator.Models.Basket
{
    public class BasketModel
    {
        public BasketModel()
        {
            BasketItems = new List<BasketItemModel>();
        }

        public string UserId { get; set; }
        public List<BasketItemModel> BasketItems { get; set; }
        public decimal TotalPrice
        {
            get => BasketItems.Sum(x => x.Price * x.Quantity);
        }
    }
}
