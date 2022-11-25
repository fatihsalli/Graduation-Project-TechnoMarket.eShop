﻿using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Catalog.Services.Interfaces
{
    public interface IProductService
    {
        public Task<CustomResponseDto<List<ProductDto>>> GetAllAsync();
        public Task<CustomResponseDto<ProductDto>> GetByIdAsync(string id);
        public Task<CustomResponseDto<ProductCreateDto>> CreateAsync(ProductCreateDto productCreateDto);
        public Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto productUpdateDto);
        public Task<CustomResponseDto<NoContentDto>> DeleteAsyn(string id);
    }
}