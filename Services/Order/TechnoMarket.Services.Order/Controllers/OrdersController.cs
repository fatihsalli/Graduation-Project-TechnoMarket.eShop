using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Services.Order.Dtos;
using TechnoMarket.Services.Order.Exceptions;
using TechnoMarket.Services.Order.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;
using TechnoMarket.Shared.Dtos;
using ZstdSharp;

namespace TechnoMarket.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomResponseDto<List<OrderDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var orderDtos=await _orderService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<List<OrderDto>>.Success(200, orderDtos));
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            var orderDto=await _orderService.GetByIdAsync(id);

            if (orderDto == null)
            {
                //loglama
                throw new NotFoundException($"Order with id ({id}) didn't find in the database.");
            }

            return CreateActionResult(CustomResponseDto<OrderDto>.Success(200, orderDto));
        }

        [HttpGet]
        [Route("/api/[controller]/[action]/{customerId}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<List<OrderDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCustomerId(string customerId)
        {
            var orderDtos = await _orderService.GetByCustomerIdAsync(customerId);

            if (orderDtos.Count<1)
            {
                //loglama
                throw new NotFoundException($"Order with customerId ({customerId}) didn't find in the database.");
            }

            return CreateActionResult(CustomResponseDto<List<OrderDto>>.Success(200, orderDtos));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponseDto<OrderDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto orderCreateDto)
        {
            var orderDto=await _orderService.CreateAsync(orderCreateDto);
            return CreateActionResult(CustomResponseDto<OrderDto>.Success(201, orderDto));
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(string id,[FromBody] OrderUpdateDto orderUpdateDto)
        {
            var orderCheck=await _orderService.GetByIdAsync(id);

            if (orderCheck == null)
            {
                //loglama
                throw new NotFoundException($"Order with id ({id}) didn't find in the database.");
            }

            var orderDto = await _orderService.UpdateAsync(orderUpdateDto, id);            
            return CreateActionResult(CustomResponseDto<OrderDto>.Success(200, orderDto));
        }

        [HttpPut]
        [Route("/api/[controller]/[action]")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ChangeStatus([FromBody] OrderStatusUpdateDto orderStatusUpdateDto)
        {
            var orderCheck = await _orderService.GetByIdAsync(orderStatusUpdateDto.Id);

            if (orderCheck == null)
            {
                //loglama
                throw new NotFoundException($"Order with id ({orderStatusUpdateDto.Id}) didn't find in the database.");
            }

            await _orderService.ChangeStatusAsync(orderStatusUpdateDto);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(string id)
        {
            var orderCheck =await _orderService.GetByIdAsync(id);

            if (orderCheck == null)
            {
                //loglama
                throw new NotFoundException($"Order with id ({id}) didn't find in the database.");
            }
            await _orderService.DeleteAsync(id);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
