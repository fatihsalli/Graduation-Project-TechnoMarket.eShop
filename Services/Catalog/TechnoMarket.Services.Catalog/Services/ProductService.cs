using AutoMapper;
using MongoDB.Driver;
using TechnoMarket.Services.Catalog.Data.Interfaces;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Catalog.Services
{
    public class ProductService : IProductService
    {
        private readonly ICatalogContext _context;
        private readonly IMapper _mapper;
        public ProductService(ICatalogContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var productsEntity = await _context.Products.Find(p => true).ToListAsync();

            var productsToReturn = _mapper.Map<List<ProductDto>>(productsEntity);

            return CustomResponseDto<List<ProductDto>>.Success(200, productsToReturn);
        }

        public async Task<CustomResponseDto<ProductDto>> GetByIdAsync(string id)
        {
            var productEntity = await _context.Products.Find(x => x.Id == id).SingleOrDefaultAsync();

            if (productEntity == null)
            {
                return CustomResponseDto<ProductDto>.Fail(404, $"Product ({id}) not found!");
            }

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            return CustomResponseDto<ProductDto>.Success(200, productToReturn);
        }

        public async Task<CustomResponseDto<ProductDto>> CreateAsync(ProductCreateDto productCreateDto)
        {
            var productEntity = _mapper.Map<Product>(productCreateDto);

            productEntity.CreatedAt = DateTime.Now;

            await _context.Products.InsertOneAsync(productEntity);

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            return CustomResponseDto<ProductDto>.Success(200, productToReturn);
        }

        //TODO: Update ettikten sonra datayı kaydettiğimizde CreatedAt tarihimiz null olarak içeriye basılıyor ve değiştiriliyor.
        public async Task<CustomResponseDto<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var productEntity = _mapper.Map<Product>(productUpdateDto);

            productEntity.UpdatedAt = DateTime.Now;

            var result = await _context.Products.FindOneAndReplaceAsync(x => x.Id == productUpdateDto.Id, productEntity);

            if (result == null)
            {
                return CustomResponseDto<ProductDto>.Fail(404, $"Product ({productUpdateDto.Id}) not found!");
            }

            //TODO: İşlem başarılı olma durumunda burada event göndereceğiz ileride!!! Eventual Consistency

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            return CustomResponseDto<ProductDto>.Success(200, productToReturn);
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteAsync(string id)
        {
            var result = await _context.Products.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount < 1)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, $"Product ({id}) not found!");
            }

            return CustomResponseDto<NoContentDto>.Success(200);
        }





    }
}
