using BurgerApp.Services.Abstraction;
using BurgerApp.ViewModels.OrderViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BurgerApp.Web.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        private IBurgerService _burgerService;
        private ILocationService _locationService;

        public OrderController(IOrderService orderService,
                               IBurgerService burgerService,
                               ILocationService locationService)
        {
            _orderService = orderService;
            _burgerService = burgerService;
            _locationService = locationService;

        }

        [HttpGet]
        public IActionResult Index()
        {
            List<OrderListViewModel> orderListViewModels = _orderService.GetAllOrders();
            return View(orderListViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            OrderViewModel orderViewModel = new OrderViewModel();
            ViewBag.Locations = _locationService.GetLocationsForDropdown();
            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult Create(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Locations = _locationService.GetLocationsForDropdown();
                return View(orderViewModel);
            }

            try
            {
                _orderService.CreateOrder(orderViewModel);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }
            try
            {
                OrderViewModel orderViewModel = _orderService.GetOrderForEditing(id.Value);
                ViewBag.Locations = _locationService.GetLocationsForDropdown();
                return View(orderViewModel);
            }
            catch (Exception e)
            {
                return View("ResourceNotFound");
            }
        }

        [HttpPost]
        public IActionResult Edit(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Locations = _locationService.GetLocationsForDropdown();
                return View(orderViewModel);
            }

            try
            {
                _orderService.EditOrder(orderViewModel);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ResourceNotFound");
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }

            try
            {
                OrderViewModel orderViewModel = _orderService.GetOrderDetails(id.Value);
                return View(orderViewModel);
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        [HttpPost]
        public IActionResult Delete(OrderViewModel orderViewModel)
        {
            try
            {
                _orderService.DeleteOrder(orderViewModel.Id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        [HttpGet]
        public IActionResult AddBurgerToEdit(int id)
        {
            BurgerOrderViewModel burgerOrderViewModel = new BurgerOrderViewModel();
            burgerOrderViewModel.OrderId = id;
            ViewBag.Burgers = _burgerService.GetBurgersForDropdown();
            return View(burgerOrderViewModel);
        }

        [HttpPost]
        public IActionResult AddBurgerToEdit(BurgerOrderViewModel burgerOrderViewModel)
        {
            try
            {
                _orderService.AddBurger(burgerOrderViewModel);
                return RedirectToAction("Edit", new { id = burgerOrderViewModel.OrderId });
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        [HttpGet]
        public IActionResult DeleteBurgerFromEdit(int id)
        {
            BurgerOrderViewModel burgerOrderViewModel = new BurgerOrderViewModel();
            burgerOrderViewModel.OrderId = id;
            ViewBag.Burgers = _orderService.GetAllBurgers();
            return View(burgerOrderViewModel);
        }

        [HttpPost]
        public IActionResult DeleteBurgerFromEdit(BurgerOrderViewModel burgerOrderViewModel)
        {
            try
            {
                _orderService.DeleteBurger(burgerOrderViewModel);
                return RedirectToAction("Edit", new { id = burgerOrderViewModel.OrderId });
            }
            catch (Exception e)
            {
                ViewBag.OrderId = burgerOrderViewModel.OrderId;
                return View("BurgerNotFound");
            }
        }
    }
}