using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Catalog.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<CustomResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto);
        public Task<CustomResponseDto<List<CategoryDto>>> GetAllAsync();

    }
}
