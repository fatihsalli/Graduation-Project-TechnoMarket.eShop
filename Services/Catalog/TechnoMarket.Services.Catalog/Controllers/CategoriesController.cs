using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Services;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomResponseDto<List<CategoryDto>>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            var categoryDtos = _categoryService.GetAll();
            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoryDtos));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponseDto<CategoryDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto categoryCreateDto)
        {
            var categoryDto = await _categoryService.AddAsync(categoryCreateDto);
            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(201, categoryDto));
        }

        [HttpPut]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            await _categoryService.UpdateAsync(categoryUpdateDto);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id:length(36)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(string id)
        {
            await _categoryService.RemoveAsync(id);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }




    }
}
