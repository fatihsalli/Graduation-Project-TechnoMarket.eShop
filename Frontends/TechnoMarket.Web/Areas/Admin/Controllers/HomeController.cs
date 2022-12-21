using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ICustomerService _customerService;
        public HomeController(ICatalogService catalogService,ICustomerService customerService)
        {
            _catalogService = catalogService;
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _catalogService.GetAllProductsAsync();
            ViewBag.TotalProducts = products.Count;

            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.TotalCategories = categories.Count;

            var customers= await _customerService.GetAllCustomersAsync();
            ViewBag.TotalCustomers= customers.Count;

            return View();
        }
    }
}
