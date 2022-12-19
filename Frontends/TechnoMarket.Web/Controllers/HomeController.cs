using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechnoMarket.Web.Models;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogService _catalogService;

        public HomeController(ICatalogService catalogService)
        {
            _catalogService= catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var products=await _catalogService.GetAllProductsAsync();

            return View(products);
        }



    }
}