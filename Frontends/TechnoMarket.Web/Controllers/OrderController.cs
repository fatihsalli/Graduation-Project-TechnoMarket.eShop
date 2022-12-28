using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Web.Models.Order;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        private readonly ICustomerService _customerService;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IOrderService orderService, IBasketService basketService, ICustomerService customerService, UserManager<IdentityUser> userManager)
        {
            _orderService = orderService;
            _basketService = basketService;
            _customerService = customerService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);

            var basket = await _basketService.GetAsync(user.Id);

            ViewBag.Basket = basket;

            return View(new CheckoutInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInput checkoutInput)
        {
            var user = await _userManager.GetUserAsync(User);

            var customerVM = await _customerService.CreateOrder(checkoutInput);

            #region Senkron Yol
            //Senkron yol
            //var order=await _orderService.CreateOrderAsync(checkoutInput, customerVM.Id, user.Id); 
            #endregion

            //Asenkron yol
            var orderSuccess = await _basketService.CheckOutForAsyncCommunication(checkoutInput, customerVM.Id, user.Id);

            await _basketService.DeleteAsycn(user.Id);
            return RedirectToAction(nameof(CheckoutHistory));
        }

        public IActionResult CheckoutHistory()
        {
            return View();
        }

    }
}
