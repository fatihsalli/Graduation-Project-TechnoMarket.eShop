using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Web.Models.Catalog;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICatalogService _catalogService;
        public CategoryController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateInput categoryCreateInput)
        {
            //Model kontrol
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.CreateCategoryAsync(categoryCreateInput);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var category = await _catalogService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                //to do mesaj gösterilebilir
                return RedirectToAction(nameof(Index));
            }

            //Update kısmında formda mevcut veriler ile gitmesi için yapıldı
            CategoryUpdateInput categoryUpdateInput = new()
            {
                Id = category.Id,
                Name = category.Name,
            };

            return View(categoryUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateInput categoryUpdateInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.UpdateCategoryAsync(categoryUpdateInput);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
