﻿namespace TechnoMarket.Web.Areas.Admin.Models.Products
{
    public class ProductUpdateInput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public IFormFile PhotoFormFile { get; set; }
        public ProductFeatureCreateInput Feature { get; set; }
        public string CategoryId { get; set; }

    }
}