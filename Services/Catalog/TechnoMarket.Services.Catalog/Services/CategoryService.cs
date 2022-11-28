﻿using AutoMapper;
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
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICatalogContext context, IMapper mapper, ILogger<CategoryService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CustomResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var categoriesEntity = await _context.Categories.Find(category => true).ToListAsync();

            var categoriesToReturn = _mapper.Map<List<CategoryDto>>(categoriesEntity);

            return CustomResponseDto<List<CategoryDto>>.Success(200, categoriesToReturn);
        }

        public async Task<CustomResponseDto<CategoryDto>> GetByIdAsync(string id)
        {
            var categoryEntity = await _context.Categories.Find(x => x.Id == id).SingleOrDefaultAsync();

            if (categoryEntity == null)
            {
                _logger.LogError($"Category ({id}) not found!");
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
                _logger.LogError($"Category ({categoryUpdateDto.Id}) not found!");
                return CustomResponseDto<CategoryDto>.Fail(404, $"Category ({categoryUpdateDto.Id}) not found!");
            }

            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);

            return CustomResponseDto<CategoryDto>.Success(200, categoryToReturn);
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteAsync(string id)
        {
            var result = await _context.Categories.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount < 1)
            {
                _logger.LogError($"Category ({id}) not found!");
                return CustomResponseDto<NoContentDto>.Fail(404, $"Category ({id}) not found!");
            }

            return CustomResponseDto<NoContentDto>.Success(200);
        }


    }
}
