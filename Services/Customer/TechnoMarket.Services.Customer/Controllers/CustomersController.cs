using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Services.Customer.Dtos;
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

        [HttpGet]
        [ProducesResponseType(typeof(CustomResponseDto<List<CustomerDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var customerDtos = await _customerService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<List<CustomerDto>>.Success(200, customerDtos));
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(CustomResponseDto<List<CustomerDtoWithAddress>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllWithAddress()
        {
            var customerDtos = await _customerService.GetCustomersWithAddressAsync();
            return CreateActionResult(CustomResponseDto<List<CustomerDtoWithAddress>>.Success(200, customerDtos));
        }

        [HttpGet("{id:length(36)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<CustomerDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            var customerDto = await _customerService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(200, customerDto));
        }

        [HttpGet("[action]/{id:length(36)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<CustomerDtoWithAddress>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdWithAddress(string id)
        {
            var customerDto = await _customerService.GetByIdWithAddressAsync(id);
            return CreateActionResult(CustomResponseDto<CustomerDtoWithAddress>.Success(200, customerDto));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponseDto<CustomerDtoWithAddress>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CustomerCreateDto customerCreateDto)
        {
            var customerDto = await _customerService.AddAsync(customerCreateDto);
            return CreateActionResult(CustomResponseDto<CustomerDtoWithAddress>.Success(201, customerDto));
        }

        [HttpPut]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] CustomerUpdateDto customerUpdateDto)
        {
            await _customerService.UpdateAsync(customerUpdateDto);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id:length(36)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(string id)
        {
            await _customerService.RemoveAsync(id);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
