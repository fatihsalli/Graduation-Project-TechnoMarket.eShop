using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Services.Order.Dtos;
using TechnoMarket.Services.Order.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomResponseDto<List<OrderDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var orderDtos = await _orderService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<List<OrderDto>>.Success(200, orderDtos));
        }

        [HttpGet("{id:length(36)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            var orderDto = await _orderService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<OrderDto>.Success(200, orderDto));
        }

        [HttpGet("[action]/{customerId:length(36)}")]
        //[Route("/api/[controller]/[action]/{customerId}")] => Alternatif
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<List<OrderDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCustomerId(string customerId)
        {
            var orderDtos = await _orderService.GetByCustomerIdAsync(customerId);
            return CreateActionResult(CustomResponseDto<List<OrderDto>>.Success(200, orderDtos));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponseDto<OrderDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto orderCreateDto)
        {
            var orderDto = await _orderService.CreateAsync(orderCreateDto);
            return CreateActionResult(CustomResponseDto<OrderDto>.Success(201, orderDto));
        }

        [HttpPut]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponseDto<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] OrderUpdateDto orderUpdateDto)
        {
            var orderDto = await _orderService.UpdateAsync(orderUpdateDto);
            return CreateActionResult(CustomResponseDto<OrderDto>.Success(200, orderDto));
        }

        [HttpPut("[action]")]
        //[Route("/api/[controller]/[action]")] => Alternatif
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ChangeStatus([FromBody] OrderStatusUpdateDto orderStatusUpdateDto)
        {
            await _orderService.ChangeStatusAsync(orderStatusUpdateDto);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id:length(36)}")]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(string id)
        {
            await _orderService.DeleteAsync(id);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
