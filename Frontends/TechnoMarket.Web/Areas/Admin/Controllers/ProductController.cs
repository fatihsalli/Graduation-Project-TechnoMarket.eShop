using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechnoMarket.Web.Models.Catalog;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ICatalogService _catalogService;
        public ProductController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _catalogService.GetAllProductsAsync();
            return View(products);
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

            await _catalogService.CreateProductAsync(productCreateInput);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var product = await _catalogService.GetProductByIdAsync(id);

            if (product == null)
            {
                //to do mesaj gösterilebilir
                return RedirectToAction(nameof(Index));
            }

            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name", product.Id);

            //Update kısmında formda mevcut veriler ile gitmesi için yapıldı
            ProductUpdateInput productUpdateInput = new()
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                Description = product.Description,
                Picture = product.Picture,
                CategoryId = product.Category.Id
            };

            return View(productUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateInput productUpdateInput)
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name", productUpdateInput.Id);

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.UpdateProductAsync(productUpdateInput);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
