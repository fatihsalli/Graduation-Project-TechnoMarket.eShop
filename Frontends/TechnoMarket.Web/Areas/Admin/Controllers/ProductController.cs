using Microsoft.AspNetCore.Mvc;

namespace TechnoMarket.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {




        public IActionResult Index()
        {
            return View();
        }
    }
}
