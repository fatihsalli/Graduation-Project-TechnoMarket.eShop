using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;

namespace TechnoMarket.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAllAsync();
            return CreateActionResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response=await _productService.GetByIdAsync(id);
            return CreateActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
        {
            var response = await _productService.CreateAsync(productCreateDto);
            return CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            var response=await _productService.UpdateAsync(productUpdateDto);
            return CreateActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response=await _productService.DeleteAsync(id);
            return CreateActionResult(response);
        }

    }
}
