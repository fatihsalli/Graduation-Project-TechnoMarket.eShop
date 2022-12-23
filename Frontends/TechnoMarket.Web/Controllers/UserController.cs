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

        public IActionResult Index()
        {
            return View();
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


    }
}
