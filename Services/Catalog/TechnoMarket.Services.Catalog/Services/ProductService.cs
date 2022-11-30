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
        private readonly ILogger<ProductService> _logger;

        public ProductService(ICatalogContext context, IMapper mapper, ILogger<ProductService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
                _logger.LogError($"Product ({id}) not found!");
                return CustomResponseDto<ProductDto>.Fail(404, $"Product ({id}) not found!");
            }

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            return CustomResponseDto<ProductDto>.Success(200, productToReturn);
        }

        public async Task<CustomResponseDto<ProductDto>> CreateAsync(ProductCreateDto productCreateDto)
        {
            var productEntity = _mapper.Map<Product>(productCreateDto);

            productEntity.CreatedAt=DateTime.Now;

            await _context.Products.InsertOneAsync(productEntity);

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            return CustomResponseDto<ProductDto>.Success(200, productToReturn);
        }

        //TODO: Update ettikten sonra datayı kaydettiğimizde CreatedAt tarihimiz null olarak içeriye basılıyor ve değiştiriliyordu. Tekrar CreatedAt tarihini verdik şu an sorunsuz çalışıyor. Ama daha profesyonel bir yol bakılacak.
        public async Task<CustomResponseDto<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var productIdCheck = await _context.Products.Find(x => x.Id == productUpdateDto.Id).SingleOrDefaultAsync();

            if (productIdCheck == null)
            {
                _logger.LogError($"Product ({productUpdateDto.Id}) not found!");
                return CustomResponseDto<ProductDto>.Fail(404, $"Product ({productUpdateDto.Id}) not found!");
            }

            var productEntity = _mapper.Map<Product>(productUpdateDto);

            productEntity.UpdatedAt = DateTime.Now;
            productEntity.CreatedAt=productIdCheck.CreatedAt;

            var result = await _context.Products.FindOneAndReplaceAsync(x => x.Id == productUpdateDto.Id, productEntity);

            //TODO: İşlem başarılı olma durumunda burada event göndereceğiz ileride!!! Eventual Consistency

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            return CustomResponseDto<ProductDto>.Success(200, productToReturn);
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteAsync(string id)
        {
            var result = await _context.Products.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount < 1)
            {
                _logger.LogError($"Product ({id}) not found!");
                return CustomResponseDto<NoContentDto>.Fail(404, $"Product ({id}) not found!");
            }

            return CustomResponseDto<NoContentDto>.Success(200);
        }





    }
}
