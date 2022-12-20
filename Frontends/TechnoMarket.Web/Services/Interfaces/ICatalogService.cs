using TechnoMarket.Web.Areas.Admin.Models;
using TechnoMarket.Web.Models.Catalog;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<ProductVM>> GetAllProductsAsync();
        Task<ProductVM> GetProductByIdAsync(string id);
        Task<bool> CreateCourseAsync(ProductCreateInput productCreateInput);

        Task<List<CategoryVM>> GetAllCategoriesAsync();
    }
}
