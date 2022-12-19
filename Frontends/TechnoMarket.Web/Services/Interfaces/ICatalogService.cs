using TechnoMarket.Web.Models.Catalog;

namespace TechnoMarket.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        public Task<List<ProductVM>> GetAllProductsAsync();


    }
}
