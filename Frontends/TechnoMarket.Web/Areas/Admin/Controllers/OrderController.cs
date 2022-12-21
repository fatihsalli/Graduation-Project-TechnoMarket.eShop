using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _orderService.GetAllOrdersAsync();
            return View(customers);
        }
    }
}
