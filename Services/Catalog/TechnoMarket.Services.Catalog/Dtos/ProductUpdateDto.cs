﻿namespace TechnoMarket.Services.Catalog.Dtos
{
    public class ProductUpdateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public ProductFeatureDto Feature { get; set; }
        public string CategoryId { get; set; }

    }
}
