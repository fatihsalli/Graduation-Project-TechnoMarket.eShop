using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Customer.Repositories.Interfaces;
using TechnoMarket.Services.Customer.Services.Interfaces;
using TechnoMarket.Services.Customer.UnitOfWorks.Interfaces;
using TechnoMarket.Shared.Exceptions;

namespace TechnoMarket.Services.Customer.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Models.Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IMapper mapper,IGenericRepository<Models.Customer> repository,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers=await _repository.GetAll().ToListAsync();
            return _mapper.Map<List<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetByIdAsync(string id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
            {
                //Loglama
                throw new NotFoundException($"Customer with id ({id}) didn't find in the database.");
            }

            return _mapper.Map<CustomerDto>(customer);
        }


        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<CustomerDto> AddAsync(CustomerCreateDto customerCreateDto)
        {
            var customer = _mapper.Map<Models.Customer>(customerCreateDto);

            //Id'yi AddAsycn metotu esnasında EF Core otomatik oluşturuyor.
            customer.CreatedAt = DateTime.Now;

            await _repository.AddAsync(customer);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CustomerDto>(customer);
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
