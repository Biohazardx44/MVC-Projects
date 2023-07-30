using BurgerApp.Services.Abstraction;
using BurgerApp.ViewModels.HomeViewModels;
using BurgerApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BurgerApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private IOrderService _orderService;
        private IBurgerService _burgerService;
        private ILocationService _locationService;

        public HomeController(IOrderService orderService,
                              IBurgerService burgerService,
                              ILocationService locationService)
        {
            _orderService = orderService;
            _burgerService = burgerService;
            _locationService = locationService;
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for the home page.
        /// It retrieves the data needed to display on the home page, such as most popular burgers,
        /// total order count, average order price, and burger locations, and returns the "Index" view.
        /// </summary>
        /// <returns>The "Index" view with the required data.</returns>
        public IActionResult Index()
        {
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel();
            homeIndexViewModel.MostPopularBurgers = _burgerService.GetMostPopularBurgers();
            homeIndexViewModel.OrderCount = _orderService.GetAllOrders().Count;
            homeIndexViewModel.OrderAveragePrice = _orderService.AverageOrderPrice();
            homeIndexViewModel.BurgerLocations = _locationService.GetAllLocations();
            return View(homeIndexViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}