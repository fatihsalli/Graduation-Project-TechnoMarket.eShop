using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Catalog.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<CustomResponseDto<List<CategoryDto>>> GetAllAsync();
        public Task<CustomResponseDto<CategoryDto>> GetByIdAsync(string id);
        public Task<CustomResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto);
        public Task<CustomResponseDto<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        public Task<CustomResponseDto<NoContentDto>> DeleteAsync(string id);
    }
}
