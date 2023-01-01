namespace TechnoMarket.Web.Models.Catalog
{
    public class ProductVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string StockPictureUrl { get; set; }
        public string ProductFeature { get; set; }
        public CategoryVM Category { get; set; }
    }
}
