using Microsoft.AspNetCore.Mvc;

namespace Recipes.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
