using Microsoft.AspNetCore.Mvc;
using PizzaApp.Services.Abstraction;
using PizzaApp.ViewModels.HomeViewModels;
using PizzaApp.Web.Models;
using System.Diagnostics;

namespace PizzaApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private IPizzaService _pizzaService;
        private IOrderService _orderService;

        public HomeController(IPizzaService pizzaService,
                              IOrderService orderService)
        {
            _pizzaService = pizzaService;
            _orderService = orderService;
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for the home page.
        /// It retrieves the data needed to display on the home page, such as pizzas on promotion,
        /// and total order count, and returns the "Index" view.
        /// </summary>
        /// <returns>The "Index" view with the required data.</returns>
        public IActionResult Index()
        {
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel();
            homeIndexViewModel.PizzasOnPromotion = _pizzaService.GetPizzaNamesOnPromotion();
            homeIndexViewModel.OrderCount = _orderService.GetAllOrders().Count;
            return View(homeIndexViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}