namespace TechnoMarket.Shopping.Aggregator.Models
{
    public class ShoppingModel
    {
        public string UserId { get; set; }
        public BasketModel BasketWithProducts { get; set; }
        public IEnumerable<OrderResponseModel> Orders { get; set; }

    }
}
