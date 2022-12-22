using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Web.Models.Order;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        public OrderController(IOrderService orderService, IBasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();
            ViewBag.Basket = basket;

            return View(new CheckoutInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInput checkoutInput)
        {
            var orderVM = await _orderService.CreateOrder(checkoutInput);
            return RedirectToAction(nameof(CheckoutHistory), orderVM);
        }

        public IActionResult CheckoutHistory(OrderVM orderVM)
        {
            return View(orderVM);
        }

    }
}
