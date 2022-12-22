using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TechnoMarket.Web.Models.Basket;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly ICatalogService _catalogService;
        public BasketController(IBasketService basketService, ICatalogService catalogService)
        {
            _basketService = basketService;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var basketVM = await _basketService.Get();
            return View(basketVM);
        }

        public async Task<IActionResult> AddBasketItem(string productId)
        {
            var product=await _catalogService.GetProductByIdAsync(productId);

            var basketItemVM = new BasketItemVM
            {
                ProductId = product.Id,
                Price = product.Price,
                ProductName = product.Name
            };

            await _basketService.AddBasketItem(basketItemVM);
            return RedirectToAction(nameof(Index),"Home");
        }

        public async Task<IActionResult> RemoveBasketItem(string productId)
        {
            await _basketService.RemoveBasketItem(productId);
            return RedirectToAction(nameof(Index));
        }

    }
}
