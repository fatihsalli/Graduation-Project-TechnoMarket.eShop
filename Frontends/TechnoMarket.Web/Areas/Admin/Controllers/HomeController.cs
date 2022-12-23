using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        public HomeController(ICatalogService catalogService, ICustomerService customerService, IOrderService orderService)
        {
            _catalogService = catalogService;
            _customerService = customerService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _catalogService.GetAllProductsAsync();
            ViewBag.TotalProducts = products.Count;

            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.TotalCategories = categories.Count;

            var customers = await _customerService.GetAllCustomersAsync();
            ViewBag.TotalCustomers = customers.Count;

            var orders = await _orderService.GetAllOrdersAsync();
            ViewBag.TotalOrders = orders.Count;

            return View();
        }
    }
}
