using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Web.Models.Auth;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ICatalogService catalogService, IBasketService basketService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ICustomerService customerService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var basket = await _basketService.GetAsync(user.Id);
                if (basket != null)
                {
                    TempData["BasketCount"] = basket.BasketItems.Count;
                }
            }

            var products = await _catalogService.GetAllProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> Detail(string id)
        {
            return View(await _catalogService.GetProductByIdAsync(id));
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterInput registerInput)
        {
            if (!ModelState.IsValid)
            {
                return View(registerInput);
            }

            var newUser = new IdentityUser()
            {
                UserName = registerInput.Username,
                Email = registerInput.Email
            };

            var result = await _userManager.CreateAsync(newUser, registerInput.Password);

            if (!result.Succeeded)
            {
                //Loglama
                throw new Exception("User can't create");
            }

            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                await _roleManager.CreateAsync(new() { Name = "admin" });
            }

            //Role tanımlama
            if (newUser.Email == "sallifatih@hotmail.com")
            {
                await _userManager.AddToRoleAsync(newUser, "Admin");
            }

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInput loginInput)
        {
            if (!ModelState.IsValid)
            {
                return View(loginInput);
            }

            var user = await _userManager.FindByEmailAsync(loginInput.Email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginInput.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UserInfoPage()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

    }
}