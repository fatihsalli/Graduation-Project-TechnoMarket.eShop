﻿namespace TechnoMarket.Shopping.Aggregator.Models
{
    public class CatalogModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public string ProductFeature { get; set; }
        public string CategoryId { get; set; }
    }
}