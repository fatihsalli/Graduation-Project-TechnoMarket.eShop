using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Web.Models.Customer;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ICustomerService _customerService;
        public UserController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerCreateInputWithRegister register)
        {
            var result = await _customerService.RegisterCustomer(register);

            if (!result)
            {
                return View();
            }

            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInput loginInput)
        {
            var result = await _customerService.LoginUser(loginInput);

            if (!result)
            {
                return View();
                
            }

            return RedirectToAction(nameof(Index), "Home");
        }


    }
}
