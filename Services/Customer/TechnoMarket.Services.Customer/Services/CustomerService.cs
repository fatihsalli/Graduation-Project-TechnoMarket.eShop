using AutoMapper;
using System.Linq.Expressions;
using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Customer.Repositories.Interfaces;
using TechnoMarket.Services.Customer.Services.Interfaces;
using TechnoMarket.Services.Customer.UnitOfWorks.Interfaces;

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

        public async Task<CustomerDto> AddAsync(CustomerCreateDto customerCreateDto)
        {
            var customer = _mapper.Map<Models.Customer>(customerCreateDto);

            //TODO: Id oluşturma işini Database tarafına aktarmak. Ancak serial olarak değil Uuid olarak.
            customer.CreatedAt = DateTime.Now;
            await _repository.AddAsync(customer);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CustomerDto>(customer);
        }







    }
}
