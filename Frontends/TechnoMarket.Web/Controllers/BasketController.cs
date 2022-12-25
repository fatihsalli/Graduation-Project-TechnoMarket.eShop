using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Web.Models.Basket;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly ICatalogService _catalogService;
        private readonly UserManager<IdentityUser> _userManager;

        public BasketController(IBasketService basketService, ICatalogService catalogService, UserManager<IdentityUser> userManager)
        {
            _basketService = basketService;
            _catalogService = catalogService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var basketVM = await _basketService.GetAsync(user.Id);

            return View(basketVM);
        }

        public async Task<IActionResult> AddBasketItem(string productId)
        {
            var product = await _catalogService.GetProductByIdAsync(productId);

            var basketItemVM = new BasketItemVM
            {
                ProductId = product.Id,
                Price = product.Price,
                ProductName = product.Name
            };

            var user = await _userManager.GetUserAsync(User);

            await _basketService.AddBasketItemAsycn(basketItemVM, user.Id);

            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> RemoveBasketItem(string productId)
        {
            var user = await _userManager.GetUserAsync(User);

            await _basketService.RemoveBasketItemAsycn(productId, user.Id);

            return RedirectToAction(nameof(Index));
        }

    }
}
