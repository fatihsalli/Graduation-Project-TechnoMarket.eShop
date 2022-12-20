﻿using TechnoMarket.Web.Areas.Admin.Models.Products;
using TechnoMarket.Web.Models.Catalog;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        //=> For products
        Task<List<ProductVM>> GetAllProductsAsync();
        Task<ProductVM> GetProductByIdAsync(string id);
        Task<bool> CreateProductAsync(ProductCreateInput productCreateInput);
        Task<bool> UpdateProductAsync(ProductUpdateInput productUpdateInput);
        Task<bool> DeleteProductAsync(string id);

        //=> For categories
        Task<List<CategoryVM>> GetAllCategoriesAsync();
        Task<CategoryVM> GetCategoryByIdAsync(string id);
    }
}
