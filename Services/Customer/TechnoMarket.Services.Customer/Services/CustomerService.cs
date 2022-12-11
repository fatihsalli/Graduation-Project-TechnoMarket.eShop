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
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IMapper mapper, ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var customers = await _repository.GetAll().ToListAsync();
            return _mapper.Map<List<CustomerDto>>(customers);
        }

        public async Task<List<CustomerDtoWithAddress>> GetCustomersWithAddressAsync()
        {
            var customers = await _repository.GetCustomersWithAddressAsync();
            return _mapper.Map<List<CustomerDtoWithAddress>>(customers);
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

        public async Task<CustomerDtoWithAddress> GetByIdWithAddressAsync(string id)
        {
            var customer = await _repository.GetSingleCustomerByIdWithAddressAsync(id);

            if (customer == null)
            {
                //Loglama
                throw new NotFoundException($"Customer with id ({id}) didn't find in the database.");
            }

            return _mapper.Map<CustomerDtoWithAddress>(customer);
        }

        public async Task<CustomerDto> AddAsync(CustomerCreateDto customerCreateDto)
        {
            var customer = _mapper.Map<Models.Customer>(customerCreateDto);
            customer.Id = Guid.NewGuid().ToString();
            await _repository.AddAsync(customer);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateAsync(CustomerUpdateDto customerUpdateDto)
        {
            var customerCurrent = await _repository.GetSingleCustomerByIdWithAddressAsync(customerUpdateDto.Id);

            if (customerCurrent == null)
            {
                //Loglama
                throw new NotFoundException($"Customer with id ({customerUpdateDto.Id}) didn't find in the database.");
            }

            var customerUpdate = _mapper.Map<Models.Customer>(customerUpdateDto);
            customerUpdate.Address.Id = customerCurrent.Address.Id;

            _repository.Update(customerUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(string id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
            {
                //Loglama
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
