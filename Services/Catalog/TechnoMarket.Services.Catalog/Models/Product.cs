namespace TechnoMarket.Services.Catalog.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ProductFeature Feature { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
