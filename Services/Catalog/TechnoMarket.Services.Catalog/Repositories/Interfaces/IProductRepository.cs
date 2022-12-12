using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Repositories.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategoryAndFeaturesAsync();
        Task<Product> GetSingleCustomerByIdWithCategoryAndFeaturesAsync(string productId);

    }
}
