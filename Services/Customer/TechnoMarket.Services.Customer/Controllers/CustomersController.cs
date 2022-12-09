using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Customer.Models;
using TechnoMarket.Services.Customer.Repositories.Interfaces;
using TechnoMarket.Shared.ControllerBases;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : CustomBaseController
    {
        private readonly IGenericRepository<Models.Customer> _customerRepository;


        public CustomersController(IGenericRepository<Models.Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerDto customerDto)
        {
            var customer = new Models.Customer();
            customer.Name = customerDto.Name;
            customer.Email = customerDto.Email;



            await _customerRepository.AddAsync(customer);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }



    }
}
