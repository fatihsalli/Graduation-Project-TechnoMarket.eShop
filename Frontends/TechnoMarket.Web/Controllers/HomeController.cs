using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public HomeController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            var basket = await _basketService.Get();

            if (basket != null)
            {
                TempData["BasketCount"] = basket.BasketItems.Count;
            }

            var products = await _catalogService.GetAllProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> Detail(string id)
        {
            return View(await _catalogService.GetProductByIdAsync(id));
        }



    }
}