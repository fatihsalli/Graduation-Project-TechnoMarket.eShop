using System.Linq.Expressions;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductWithCategoryDto>> GetAllAsync();
        Task<ProductWithCategoryDto> GetByIdAsync(string id);
        Task<ProductDto> AddAsync(ProductCreateDto productCreateDto);
        Task UpdateAsync(ProductUpdateDto productUpdateDto);
        Task RemoveAsync(string id);
        IQueryable<Product> Where(Expression<Func<Product, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<Product, bool>> expression);

    }
}
