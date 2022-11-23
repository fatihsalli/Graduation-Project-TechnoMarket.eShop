namespace TechnoMarket.Services.Catalog.Dtos
{
    public class ProductCreateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public string ImageFile { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }



    }
}
