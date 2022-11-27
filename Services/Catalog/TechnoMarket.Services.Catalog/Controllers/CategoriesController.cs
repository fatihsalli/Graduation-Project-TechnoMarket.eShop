using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Services;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;

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
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return CreateActionResult(response);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return CreateActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto categoryCreateDto)
        {
            var response = await _categoryService.CreateAsync(categoryCreateDto);
            return CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            var response = await _categoryService.UpdateAsync(categoryUpdateDto);
            return CreateActionResult(response);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _categoryService.DeleteAsync(id);
            return CreateActionResult(response);
        }




    }
}
