using Microsoft.AspNetCore.Mvc;

namespace BurgerApp.Web.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
