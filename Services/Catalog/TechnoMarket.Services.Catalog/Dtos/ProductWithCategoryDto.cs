namespace TechnoMarket.Services.Catalog.Dtos
{
    public class ProductWithCategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public string ProductFeature { get; set; }
        public CategoryDto Category { get; set; }

    }
}
