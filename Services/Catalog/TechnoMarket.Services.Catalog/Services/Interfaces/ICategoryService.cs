using TechnoMarket.Services.Catalog.Dtos;

namespace TechnoMarket.Services.Catalog.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto> GetByIdAsync(string id);
        Task<CategoryDto> AddAsync(CategoryCreateDto categoryCreateDto);
        Task UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task RemoveAsync(string id);
    }
}
