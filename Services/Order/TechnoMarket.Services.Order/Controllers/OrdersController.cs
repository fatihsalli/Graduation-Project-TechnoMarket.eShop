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
        private readonly IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomResponseDto<List<OrderDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var orders=await _orderService.GetAllAsync();

            var ordersDto=_mapper.Map<List<OrderDto>>(orders);

            return CreateActionResult(CustomResponseDto<List<OrderDto>>.Success(200,ordersDto));
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            var order=await _orderService.GetByIdAsync(id);

            if (order==null)
            {
                //loglama
                throw new Exception($"Order with id: {id} doesn't found in the database.");
            }

            var orderDto= _mapper.Map<OrderDto>(order);
            
            return CreateActionResult(CustomResponseDto<OrderDto>.Success(200, orderDto));
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<List<OrderDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCustomerId(string customerId)
        {
            var orders = await _orderService.GetByCustomerIdAsync(customerId);

            if (orders == null)
            {
                //loglama
                throw new Exception($"Order or orders with customerId: {customerId} doesn't found in the database.");
            }

            var orderDto = _mapper.Map<List<OrderDto>>(orders);

            return CreateActionResult(CustomResponseDto<List<OrderDto>>.Success(200, orderDto));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponseDto<OrderDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto orderCreateDto)
        {
            var order=_mapper.Map<Models.Order>(orderCreateDto);

            var orderToReturn=await _orderService.CreateAsync(order);

            var orderDto= _mapper.Map<OrderDto>(orderToReturn);

            return CreateActionResult(CustomResponseDto<OrderDto>.Success(200, orderDto));
        }









    }
}
