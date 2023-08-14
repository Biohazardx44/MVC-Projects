using Microsoft.AspNetCore.Mvc;

namespace PizzaApp.Web.Controllers
{
    public class AboutUsController : Controller
    {
        /// <summary>
        /// Action method that handles the HTTP GET request for the About Us page.
        /// It returns the "Index" view, which displays the About Us content.
        /// </summary>
        /// <returns>The "Index" view.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}