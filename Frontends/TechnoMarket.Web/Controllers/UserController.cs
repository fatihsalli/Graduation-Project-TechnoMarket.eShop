using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Security;
using TechnoMarket.Web.Models.Customer;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly SignInManager<IdentityUser> _signInManager;
        public UserController(ICustomerService customerService, SignInManager<IdentityUser> signInManager)
        {
            _customerService = customerService;
            _signInManager = signInManager;
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
        [ValidateAntiForgeryToken]
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
