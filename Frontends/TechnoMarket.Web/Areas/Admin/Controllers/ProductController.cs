using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechnoMarket.Web.Areas.Admin.Models;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ICatalogService _catalogService;
        public ProductController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoriesAsync();

            //Kullanıcı seçenek üzerinden ekleyebilmesi için
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateInput productCreateInput)
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");

            //Model kontrol
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.CreateCourseAsync(productCreateInput);
            return RedirectToAction("Index","Home");
        }



    }
}
