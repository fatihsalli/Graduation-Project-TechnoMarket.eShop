using AutoMapper;
using System.Linq.Expressions;
using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Customer.Repositories.Interfaces;
using TechnoMarket.Services.Customer.Services.Interfaces;

namespace TechnoMarket.Services.Customer.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Models.Customer> _repository;

        public CustomerService(IMapper mapper,IGenericRepository<Models.Customer> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CustomerDto> AddAsync(CustomerCreateDto customerCreateDto)
        {
            var customer = _mapper.Map<Models.Customer>(customerCreateDto);

            customer.Id = Guid.NewGuid().ToString();
            customer.CreatedAt = DateTime.Now;
            customer.Address.Id= Guid.NewGuid().ToString();
            customer.Address.CustomerId = customer.Id;

            await _repository.AddAsync(customer);
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
