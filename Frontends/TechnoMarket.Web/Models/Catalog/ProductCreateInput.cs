namespace TechnoMarket.Web.Models.Catalog
{
    public class ProductCreateInput
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public IFormFile PhotoFormFile { get; set; }
        public ProductFeatureCreateInput Feature { get; set; }
        public string CategoryId { get; set; }
    }
}
