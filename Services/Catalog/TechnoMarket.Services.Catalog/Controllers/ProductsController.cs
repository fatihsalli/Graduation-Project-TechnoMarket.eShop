using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Filters;
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

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomResponseDto<List<ProductDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var productDtos = await _productService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));
        }

        //Service kısmında global exception fırlatıyorum. Burada öğrenme amaçlı yapıldı. Filter sayesinde action içerise girmeden geri dönecektir.
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        //
        [HttpGet("{id:length(36)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            var productDto = await _productService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        //TODO: Filter yazılacak categoryId yok ise direkt içine girmeden hata versin
        [HttpPost]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<ProductDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productCreateDto)
        {
            var productDto = await _productService.AddAsync(productCreateDto);
            //=> Aşağıdaki gibi URI dönmek için de yazabilirdik. O zaman yukarıdaki ilgili action metota name ile yazmalıyız. İsim aynı olsa bile yazmak zorundayız.
            //return CreatedAtRoute("GetProduct", new { id = productDto.Id }, productDto);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productDto));
        }

        [HttpPut]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update([FromBody] ProductUpdateDto productUpdateDto)
        {
            await _productService.UpdateAsync(productUpdateDto);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id:length(36)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(string id)
        {
            await _productService.RemoveAsync(id);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
