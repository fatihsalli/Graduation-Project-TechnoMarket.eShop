namespace TechnoMarket.Services.Catalog.Models
{
    public class ProductFeature
    {
        public Guid Id { get; set; }
        public string Color { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Weight { get; set; }
        public Product Product { get; set; }
    }
}
