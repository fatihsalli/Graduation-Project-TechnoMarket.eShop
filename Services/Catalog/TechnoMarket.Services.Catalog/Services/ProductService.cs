using AutoMapper;
using MongoDB.Driver;
using TechnoMarket.Services.Catalog.Data.Interfaces;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Catalog.Services
{
    public class ProductService:IProductService
    {
        private readonly ICatalogContext _context;
        private readonly IMapper _mapper;
        public ProductService(ICatalogContext context,IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var products=await _context.Products.Find(p=> true).ToListAsync();

            if (products.Any())
            {
                foreach (var product in products)
                {
                    product.Category = await _context.Categories.Find(x => x.Id == product.CategoryId).SingleOrDefaultAsync();
                }
            }

            return CustomResponseDto<List<ProductDto>>.Success(200,_mapper.Map<List<ProductDto>>(products));
        }

        public async Task<CustomResponseDto<ProductCreateDto>> CreateAsync(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            product.CreatedAt= DateTime.Now;
            await _context.Products.InsertOneAsync(product);
            return CustomResponseDto<ProductCreateDto>.Success(200,_mapper.Map<ProductCreateDto>(product));
        }









    }
}
