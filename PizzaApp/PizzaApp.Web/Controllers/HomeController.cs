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

        public IActionResult Index()
        {
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel();
            homeIndexViewModel.PizzaOnPromotion = _pizzaService.GetPizzaNameOnPromotion();
            homeIndexViewModel.OrderCount = _orderService.GetAllOrders().Count;
            return View(homeIndexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}