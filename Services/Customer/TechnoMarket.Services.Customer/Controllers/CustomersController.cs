using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Customer.Models;
using TechnoMarket.Services.Customer.Repositories.Interfaces;
using TechnoMarket.Services.Customer.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : CustomBaseController
    {
        private readonly ICustomerService _customerService;


        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }



        [HttpGet("{id:length(36)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<CustomerDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            var customerDto = await _customerService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(200, customerDto));
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerCreateDto customerCreateDto)
        {
            var customerDto=await _customerService.AddAsync(customerCreateDto);
            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(200,customerDto));
        }



    }
}
