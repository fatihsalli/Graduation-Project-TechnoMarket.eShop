using AutoMapper;
using System.Linq.Expressions;
using TechnoMarket.Services.Catalog.Data;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Repositories.Interfaces;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Services.Catalog.UnitOfWorks.Interfaces;
using TechnoMarket.Shared.Exceptions;

namespace TechnoMarket.Services.Catalog.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductService> _logger;


        public ProductService(IMapper mapper, IProductRepository repository, IUnitOfWork unitOfWork, IGenericRepository<Category> categoryRepository, ILogger<ProductService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<ProductWithCategoryDto>> GetAllAsync()
        {
            var products = await _repository.GetProductsWithCategoryAndFeaturesAsync();

            return _mapper.Map<List<ProductWithCategoryDto>>(products);
        }

        public async Task<ProductWithCategoryDto> GetByIdAsync(string id)
        {
            var product = await _repository.GetSingleProductByIdWithCategoryAndFeaturesAsync(id);

            if (product == null)
            {
                _logger.LogError($"Product with id ({id}) didn't find in the database.");
                throw new NotFoundException($"Product with id ({id}) didn't find in the database.");
            }

            return _mapper.Map<ProductWithCategoryDto>(product);
        }

        public async Task<ProductDto> AddAsync(ProductCreateDto productCreateDto)
        {
            var categoryCheck = await _categoryRepository.AnyAsync(x => x.Id == new Guid(productCreateDto.CategoryId));

            if (!categoryCheck)
            {
                _logger.LogError($"Category with id ({productCreateDto.CategoryId}) didn't find in the database.");
                throw new NotFoundException($"Category with id ({productCreateDto.CategoryId}) didn't find in the database.");
            }

            var product = _mapper.Map<Product>(productCreateDto);
            await _repository.AddAsync(product);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var productCheck = await _repository.AnyAsync(x => x.Id == new Guid(productUpdateDto.Id));

            if (!productCheck)
            {
                _logger.LogError($"Product with id ({productUpdateDto.Id}) didn't find in the database.");
                throw new NotFoundException($"Product with id ({productUpdateDto.Id}) didn't find in the database.");
            }

            var categoryCheck = await _categoryRepository.AnyAsync(x => x.Id == new Guid(productUpdateDto.CategoryId));

            if (!categoryCheck)
            {
                _logger.LogError($"Category with id ({productUpdateDto.CategoryId}) didn't find in the database.");
                throw new NotFoundException($"Category with id ({productUpdateDto.CategoryId}) didn't find in the database.");
            }
            var productUpdate = _mapper.Map<Product>(productUpdateDto);
            //Önemli!!! ProductFeature için id değerini clienttan almayıp null olarak bırakırsak EF Core state değerini Added olarak ayarlıyor o sebeple de ProductFeature update edilemiyor. (Parent üzerinden update etmek için)
            productUpdate.Feature.Id = productUpdate.Id;
            _repository.Update(productUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(string id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                _logger.LogError($"Product with id ({id}) didn't find in the database.");
                throw new NotFoundException($"Product with id ({id}) didn't find in the database.");
            }

            _repository.Remove(product);
            await _unitOfWork.CommitAsync();
        }

        //Kendi içimizde kullanmak için
        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _repository.Where(expression);
        }

        //Kendi içimizde kullanmak için
        public async Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }


    }
}
