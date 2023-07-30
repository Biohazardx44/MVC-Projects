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

        /// <summary>
        /// Action method that handles the HTTP GET request for the Order list page.
        /// It retrieves all orders from the service and displays them in the "Index" view.
        /// </summary>
        /// <returns>The "Index" view with the list of orders.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            List<OrderListViewModel> orderListViewModels = _orderService.GetAllOrders();
            return View(orderListViewModels);
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for creating a new order.
        /// It returns the "Create" view for creating a new order.
        /// </summary>
        /// <returns>The "Create" view.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            OrderViewModel orderViewModel = new OrderViewModel();
            ViewBag.Locations = _locationService.GetLocationsForDropdown();
            return View(orderViewModel);
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for creating a new order.
        /// It adds the new order to the service and redirects to the "Index" view on success.
        /// If the model state is invalid, it returns the "Create" view with validation errors.
        /// </summary>
        /// <param name="orderViewModel">The order view model containing the data for the new order.</param>
        /// <returns>Redirection to "Index" on success, or "Create" view with errors on failure.</returns>
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

        /// <summary>
        /// Action method that handles the HTTP GET request for editing an order.
        /// It retrieves the order details based on the provided ID and returns the "Edit" view with the order data.
        /// If the provided ID is null, it returns the "BadRequest" view.
        /// If the order with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="id">The ID of the order to be edited.</param>
        /// <returns>
        /// The "Edit" view with the order data if found.
        /// The "BadRequest" view if the provided ID is null.
        /// The "ResourceNotFound" view if the order with the specified ID is not found.
        /// </returns>
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

        /// <summary>
        /// Action method that handles the HTTP POST request for editing an order.
        /// It receives the edited order data from the "Edit" view and updates the order in the database.
        /// If the model state is not valid, it returns the "Edit" view with the entered data.
        /// If the update is successful, it redirects to the "Index" view.
        /// If the order with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="orderViewModel">The edited order data from the "Edit" view.</param>
        /// <returns>
        /// If successful, redirects to the "Index" view.
        /// If model state is invalid, returns the "Edit" view with entered data.
        /// If the order with the specified ID is not found, returns the "ResourceNotFound" view.
        /// </returns>
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

        /// <summary>
        /// Action method that handles the HTTP GET request for deleting an order.
        /// It retrieves the order details based on the provided ID and returns the "Delete" view with the order data.
        /// If the provided ID is null, it returns the "BadRequest" view.
        /// If the order with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="id">The ID of the order to be deleted.</param>
        /// <returns>
        /// The "Delete" view with the order data if found.
        /// The "BadRequest" view if the provided ID is null.
        /// The "ResourceNotFound" view if the order with the specified ID is not found.
        /// </returns>
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

        /// <summary>
        /// Action method that handles the HTTP POST request for deleting an order.
        /// It receives the order data from the "Delete" view and deletes the order from the database.
        /// If the deletion is successful, it redirects to the "Index" view.
        /// If the order with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// If an error occurs during the deletion process, it returns the "ExceptionPage" view.
        /// </summary>
        /// <param name="orderViewModel">The order data from the "Delete" view.</param>
        /// <returns>
        /// If successful, redirects to the "Index" view.
        /// If the order with the specified ID is not found, returns the "ResourceNotFound" view.
        /// If an error occurs, returns the "ExceptionPage" view.
        /// </returns>
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

        /// <summary>
        /// Action method that handles the HTTP GET request for adding a burger to an order for editing.
        /// It initializes a new <see cref="BurgerOrderViewModel"/> and returns the "AddBurgerToEdit" view
        /// with the order ID and a list of burgers for dropdown selection.
        /// </summary>
        /// <param name="id">The ID of the order to which the burger will be added.</param>
        /// <returns>
        /// The "AddBurgerToEdit" view with a new <see cref="BurgerOrderViewModel"/>,
        /// the order ID, and a list of burgers for dropdown selection.
        /// </returns>
        [HttpGet]
        public IActionResult AddBurgerToEdit(int id)
        {
            BurgerOrderViewModel burgerOrderViewModel = new BurgerOrderViewModel();
            burgerOrderViewModel.OrderId = id;
            ViewBag.Burgers = _burgerService.GetBurgersForDropdown();
            return View(burgerOrderViewModel);
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for adding a burger to an order for editing.
        /// It receives the burger data from the "AddBurgerToEdit" view and adds the burger to the order in the database.
        /// If the addition is successful, it redirects to the "Edit" view of the order.
        /// If an error occurs during the addition process, it returns the "ExceptionPage" view.
        /// </summary>
        /// <param name="burgerOrderViewModel">The burger data from the "AddBurgerToEdit" view.</param>
        /// <returns>
        /// If successful, redirects to the "Edit" view of the order.
        /// If an error occurs, returns the "ExceptionPage" view.
        /// </returns>
        [HttpPost]
        public IActionResult AddBurgerToEdit(BurgerOrderViewModel burgerOrderViewModel)
        {
            try
            {
                _orderService.AddBurgerToEdit(burgerOrderViewModel);
                return RedirectToAction("Edit", new { id = burgerOrderViewModel.OrderId });
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for deleting a burger from an order for editing.
        /// It initializes a new <see cref="BurgerOrderViewModel"/> and returns the "DeleteBurgerFromEdit" view
        /// with the order ID and a list of burgers for dropdown selection.
        /// </summary>
        /// <param name="id">The ID of the order from which the burger will be deleted.</param>
        /// <returns>
        /// The "DeleteBurgerFromEdit" view with a new <see cref="BurgerOrderViewModel"/>,
        /// the order ID, and a list of burgers for dropdown selection.
        /// </returns>
        [HttpGet]
        public IActionResult DeleteBurgerFromEdit(int id)
        {
            BurgerOrderViewModel burgerOrderViewModel = new BurgerOrderViewModel();
            burgerOrderViewModel.OrderId = id;
            ViewBag.Burgers = _orderService.GetAllBurgers();
            return View(burgerOrderViewModel);
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for deleting a burger from an order for editing.
        /// It receives the burger data from the "DeleteBurgerFromEdit" view and deletes the burger from the order in the database.
        /// If the deletion is successful, it redirects to the "Edit" view of the order.
        /// If the burger with the specified ID is not found, it returns the "BurgerNotFound" view.
        /// </summary>
        /// <param name="burgerOrderViewModel">The burger data from the "DeleteBurgerFromEdit" view.</param>
        /// <returns>
        /// If successful, redirects to the "Edit" view of the order.
        /// If the burger with the specified ID is not found, returns the "BurgerNotFound" view.
        /// </returns>
        [HttpPost]
        public IActionResult DeleteBurgerFromEdit(BurgerOrderViewModel burgerOrderViewModel)
        {
            try
            {
                _orderService.DeleteBurgerFromEdit(burgerOrderViewModel);
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