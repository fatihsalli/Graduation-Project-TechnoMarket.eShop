using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
        }

        //TODO: ResponseType olarak belirtme nedenimiz client istek yapmadan önce hangi kodları ve hangi şemada alacağını önceden göstermek. Burada CustomResponse döndüğümüz için kontrol edilecek.
        [HttpGet]
        [ProducesResponseType(typeof(CustomResponseDto<List<ProductDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAllAsync();
            return CreateActionResult(response);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _productService.GetByIdAsync(id);
            return CreateActionResult(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponseDto<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
        {
            var response = await _productService.CreateAsync(productCreateDto);
            return CreateActionResult(response);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            var response = await _productService.UpdateAsync(productUpdateDto);
            return CreateActionResult(response);
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _productService.DeleteAsync(id);
            return CreateActionResult(response);
        }

    }
}
