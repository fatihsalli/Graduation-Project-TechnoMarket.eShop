using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogService _catalogService;

        public HomeController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _catalogService.GetAllProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> Detail(string id)
        {
            return View(await _catalogService.GetProductByIdAsync(id));
        }



    }
}