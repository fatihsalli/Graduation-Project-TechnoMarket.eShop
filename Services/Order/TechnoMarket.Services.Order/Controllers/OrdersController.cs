using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Services.Order.Dtos;
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
                throw new Exception($"Order with id: {id} doesn't found in the database.");
            }

            return CreateActionResult(CustomResponseDto<OrderDto>.Success(200, orderDto));
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<List<OrderDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCustomerId(string customerId)
        {
            var orderDtos = await _orderService.GetByCustomerIdAsync(customerId);

            if (orderDtos == null)
            {
                //loglama
                throw new Exception($"Order or orders with customerId: {customerId} doesn't found in the database.");
            }

            return CreateActionResult(CustomResponseDto<List<OrderDto>>.Success(200, orderDtos));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponseDto<OrderDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto orderCreateDto)
        {
            var orderDto=await _orderService.CreateAsync(orderCreateDto);
            return CreateActionResult(CustomResponseDto<OrderDto>.Success(200, orderDto));
        }









    }
}
