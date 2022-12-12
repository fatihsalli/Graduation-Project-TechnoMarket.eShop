using AutoMapper;
using System.Linq.Expressions;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;
using TechnoMarket.Services.Catalog.Repositories.Interfaces;
using TechnoMarket.Services.Catalog.Services.Interfaces;
using TechnoMarket.Services.Catalog.UnitOfWorks.Interfaces;
using TechnoMarket.Shared.Exceptions;

namespace TechnoMarket.Services.Catalog.Services
{
    public class ProductService:IProductService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IMapper mapper, IGenericRepository<Product> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public List<ProductDto> GetAll()
        {
            var products = _repository.GetAll().ToList();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetByIdAsync(string id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                //Loglama
                throw new NotFoundException($"Product with id ({id}) didn't find in the database.");
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> AddAsync(ProductCreateDto productCreateDto)
        {
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
                //
                throw new NotFoundException($"Product with id ({productUpdateDto.Id}) didn't find in the database.");
            }

            var productUpdate = _mapper.Map<Product>(productUpdateDto);
            _repository.Update(productUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(string id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                //
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
