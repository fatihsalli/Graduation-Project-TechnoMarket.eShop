using Microsoft.AspNetCore.Mvc;

namespace TechnoMarket.Web.Controllers
{
    public class UserController : Controller
    {




        public IActionResult Index()
        {
            return View();
        }
    }
}
