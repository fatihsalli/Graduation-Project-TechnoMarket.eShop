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
            var categoryEntities=await _context.Categories.Find(category=> true).ToListAsync();
            var categoryDtos=_mapper.Map<List<CategoryDto>>(categoryEntities);
            return CustomResponseDto<List<CategoryDto>>.Success(200, categoryDtos);
        }

        public async Task<CustomResponseDto<CategoryDto>> GetById(string id)
        {
            var categoryEntity = await _context.Categories.Find(x => x.Id == id).SingleOrDefaultAsync();

            if (categoryEntity == null)
            {
                return CustomResponseDto<CategoryDto>.Fail(404, $"Product ({id}) not found!");
            }

            return CustomResponseDto<CategoryDto>.Success(200, _mapper.Map<CategoryDto>(categoryEntity));
        }

        public async Task<CustomResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var categoryEntity = _mapper.Map<Category>(categoryCreateDto);
            await _context.Categories.InsertOneAsync(categoryEntity);
            var categoryDto=_mapper.Map<CategoryDto>(categoryEntity);
            //Geriye Id dönmemi Client bekleyebilir.
            return CustomResponseDto<CategoryDto>.Success(201,categoryDto);
        }

        



    }
}
