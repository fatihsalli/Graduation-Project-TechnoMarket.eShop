using AutoMapper;
using MongoDB.Driver;
using TechnoMarket.Services.Catalog.Data.Interfaces;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICatalogContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ICatalogContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var categoriesEntity = await _context.Categories.Find(category => true).ToListAsync();

            var categoriesToReturn = _mapper.Map<List<CategoryDto>>(categoriesEntity);

            return CustomResponseDto<List<CategoryDto>>.Success(200, categoriesToReturn);
        }

        public async Task<CustomResponseDto<CategoryDto>> GetById(string id)
        {
            var categoryEntity = await _context.Categories.Find(x => x.Id == id).SingleOrDefaultAsync();

            if (categoryEntity == null)
            {
                return CustomResponseDto<CategoryDto>.Fail(404, $"Category ({id}) not found!");
            }

            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);

            return CustomResponseDto<CategoryDto>.Success(200, categoryToReturn);
        }

        public async Task<CustomResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var categoryEntity = _mapper.Map<Category>(categoryCreateDto);

            await _context.Categories.InsertOneAsync(categoryEntity);

            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);
            //Geriye Id dönmemi Client bekleyebilir.
            return CustomResponseDto<CategoryDto>.Success(201, categoryToReturn);
        }

        public async Task<CustomResponseDto<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var categoryEntity = _mapper.Map<Category>(categoryUpdateDto);
            var result = await _context.Categories.FindOneAndReplaceAsync(x => x.Id == categoryUpdateDto.Id, categoryEntity);

            if (result == null)
            {
                return CustomResponseDto<CategoryDto>.Fail(404, $"Category ({categoryUpdateDto.Id}) not found!");
            }

            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);

            return CustomResponseDto<CategoryDto>.Success(200, categoryToReturn);
        }



    }
}
