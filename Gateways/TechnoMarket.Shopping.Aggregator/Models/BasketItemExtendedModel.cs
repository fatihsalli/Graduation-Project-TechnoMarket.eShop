namespace TechnoMarket.Shopping.Aggregator.Models
{
    public class BasketItemExtendedModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        //Product Related Additional Fields
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public string ProductFeature { get; set; }
        public string CategoryId { get; set; }
    }
}
