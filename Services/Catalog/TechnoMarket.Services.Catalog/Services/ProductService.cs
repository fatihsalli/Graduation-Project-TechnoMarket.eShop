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

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var customers = await _repository.GetAll().ToListAsync();
            return _mapper.Map<List<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetByIdAsync(string id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
            {
                _logger.LogError($"Customer with id ({id}) didn't find in the database.");
                throw new NotFoundException($"Customer with id ({id}) didn't find in the database.");
            }

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> AddAsync(CustomerCreateDto customerCreateDto)
        {
            var customer = _mapper.Map<Models.Customer>(customerCreateDto);
            await _repository.AddAsync(customer);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateAsync(CustomerUpdateDto customerUpdateDto)
        {
            var customerCheck = await _repository.AnyAsync(x => x.Id == new Guid(customerUpdateDto.Id));

            if (!customerCheck)
            {
                _logger.LogError($"Customer with id ({customerUpdateDto.Id}) didn't find in the database.");
                throw new NotFoundException($"Customer with id ({customerUpdateDto.Id}) didn't find in the database.");
            }

            var customerUpdate = _mapper.Map<Models.Customer>(customerUpdateDto);
            _repository.Update(customerUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(string id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
            {
                _logger.LogError($"Customer with id ({id}) didn't find in the database.");
                throw new NotFoundException($"Customer with id ({id}) didn't find in the database.");
            }

            _repository.Remove(customer);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<Models.Customer> Where(Expression<Func<Models.Customer, bool>> expression)
        {
            return _repository.Where(expression);
        }

        //Kendi içimizde kullanmak için
        public async Task<bool> AnyAsync(Expression<Func<Models.Customer, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }


    }
}
