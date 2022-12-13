namespace TechnoMarket.Services.Catalog.Dtos
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public ProductFeatureDto Feature { get; set; }
        public string CategoryId { get; set; }

    }
}
