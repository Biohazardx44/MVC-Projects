using Microsoft.AspNetCore.Mvc;
using PizzaApp.Services.Abstraction;
using PizzaApp.ViewModels.OrderViewModels;

namespace PizzaApp.Web.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        private IUserService _userService;
        private IPizzaService _pizzaService;

        public OrderController(IOrderService orderService,
                               IUserService userService,
                               IPizzaService pizzaService)
        {
            _orderService = orderService;
            _userService = userService;
            _pizzaService = pizzaService;
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
        /// Action method that handles the HTTP GET request for displaying details of an order.
        /// It retrieves order details using the provided order ID and returns the "Details" view.
        /// If the order ID is null, the "BadRequest" view is returned. If an exception occurs,
        /// the method catches it, and returns the "ExceptionPage" view.
        /// </summary>
        /// <param name="id">The ID of the order to display details for.</param>
        /// <returns>The "Details" view displaying order details, or respective error views.</returns>
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }
            try
            {
                OrderDetailsViewModel orderDetailsViewModel = _orderService.GetOrderDetails(id.Value);
                return View(orderDetailsViewModel);
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for creating a new order.
        /// It returns the "Create" view for creating a new order.
        /// If no users are available for creating the order, it returns the "UserNotFoundForCreate" view.
        /// </summary>
        /// <returns>The "Create" view.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            OrderViewModel orderViewModel = new OrderViewModel();
            ViewBag.Users = _userService.GetUsersForDropdown();

            if (ViewBag.Users.Count == 0)
            {
                return View("UserNotFoundForCreate");
            }

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
                ViewBag.Users = _userService.GetUsersForDropdown();
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
                ViewBag.Users = _userService.GetUsersForDropdown();
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
        /// If the selected user for editing the order is not found, it returns the "UserNotFoundForEdit" view.
        /// </summary>
        /// <param name="orderViewModel">The edited order data from the "Edit" view.</param>
        /// <returns>
        /// If successful, redirects to the "Index" view.
        /// If model state is invalid, returns the "Edit" view with entered data.
        /// If the order with the specified ID is not found, returns the "ResourceNotFound" view.
        /// If the selected user for editing the order is not found, returns the "UserNotFoundForEdit" view.
        /// </returns>
        [HttpPost]
        public IActionResult Edit(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Users = _userService.GetUsersForDropdown();
                return View(orderViewModel);
            }

            try
            {
                var availableUserIds = _userService.GetUsersForDropdown().Select(u => u.Id);
                if (!availableUserIds.Contains(orderViewModel.UserId))
                {
                    ViewBag.OrderId = orderViewModel.Id;
                    return View("UserNotFoundForEdit");
                }

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
                OrderDetailsViewModel orderDetailsViewModel = _orderService.GetOrderDetails(id.Value);
                return View(orderDetailsViewModel);
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
        /// <param name="orderDetailsViewModel">The order data from the "Delete" view.</param>
        /// <returns>
        /// If successful, redirects to the "Index" view.
        /// If the order with the specified ID is not found, returns the "ResourceNotFound" view.
        /// If an error occurs, returns the "ExceptionPage" view.
        /// </returns>
        [HttpPost]
        public IActionResult Delete(OrderDetailsViewModel orderDetailsViewModel)
        {
            try
            {
                _orderService.DeleteOrder(orderDetailsViewModel.Id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for adding a pizza to an order for editing.
        /// It initializes a new <see cref="PizzaOrderViewModel"/> and returns the "AddPizzaToEdit" view
        /// with the order ID and a list of pizzas for dropdown selection.
        /// </summary>
        /// <param name="id">The ID of the order to which the pizza will be added.</param>
        /// <returns>
        /// The "AddPizzaToEdit" view with a new <see cref="PizzaOrderViewModel"/>,
        /// the order ID, and a list of pizzas for dropdown selection.
        /// </returns>
        [HttpGet]
        public IActionResult AddPizzaToEdit(int id)
        {
            PizzaOrderViewModel pizzaOrderViewModel = new PizzaOrderViewModel();
            pizzaOrderViewModel.OrderId = id;
            ViewBag.Pizzas = _pizzaService.GetPizzasForDropdown();
            return View(pizzaOrderViewModel);
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for adding a pizza to an order for editing.
        /// It receives the pizza data from the "AddPizzaToEdit" view and adds the pizza to the order in the database.
        /// If the addition is successful, it redirects to the "Edit" view of the order.
        /// If an error occurs during the addition process, it returns the "ExceptionPage" view.
        /// </summary>
        /// <param name="pizzaOrderViewModel">The pizza data from the "AddPizzaToEdit" view.</param>
        /// <returns>
        /// If successful, redirects to the "Edit" view of the order.
        /// If an error occurs, returns the "ExceptionPage" view.
        /// </returns>
        [HttpPost]
        public IActionResult AddPizzaToEdit(PizzaOrderViewModel pizzaOrderViewModel)
        {
            try
            {
                _orderService.AddPizzaToEdit(pizzaOrderViewModel);
                return RedirectToAction("Edit", new { id = pizzaOrderViewModel.OrderId });
            }
            catch (Exception e)
            {
                ViewBag.OrderId = pizzaOrderViewModel.OrderId;
                return View("PizzaNotFound");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for deleting a pizza from an order for editing.
        /// It initializes a new <see cref="PizzaOrderViewModel"/> and returns the "DeletePizzaFromEdit" view
        /// with the order ID and a list of pizzas for dropdown selection.
        /// </summary>
        /// <param name="id">The ID of the order from which the pizza will be deleted.</param>
        /// <returns>
        /// The "DeletePizzaFromEdit" view with a new <see cref="PizzaOrderViewModel"/>,
        /// the order ID, and a list of pizzas for dropdown selection.
        /// </returns>
        [HttpGet]
        public IActionResult DeletePizzaFromEdit(int id)
        {
            PizzaOrderViewModel pizzaOrderViewModel = new PizzaOrderViewModel();
            pizzaOrderViewModel.OrderId = id;
            ViewBag.Pizzas = _pizzaService.GetPizzasForDropdown();
            return View(pizzaOrderViewModel);
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for deleting a pizza from an order for editing.
        /// It receives the pizza data from the "DeletePizzaFromEdit" view and deletes the pizza from the order in the database.
        /// If the deletion is successful, it redirects to the "Edit" view of the order.
        /// If the pizza with the specified ID is not found, it returns the "PizzaNotFound" view.
        /// </summary>
        /// <param name="pizzaOrderViewModel">The pizza data from the "DeletePizzaFromEdit" view.</param>
        /// <returns>
        /// If successful, redirects to the "Edit" view of the order.
        /// If the pizza with the specified ID is not found, returns the "PizzaNotFound" view.
        /// </returns>
        [HttpPost]
        public IActionResult DeletePizzaFromEdit(PizzaOrderViewModel pizzaOrderViewModel)
        {
            try
            {
                _orderService.DeletePizzaFromEdit(pizzaOrderViewModel);
                return RedirectToAction("Edit", new { id = pizzaOrderViewModel.OrderId });
            }
            catch (Exception e)
            {
                ViewBag.OrderId = pizzaOrderViewModel.OrderId;
                return View("PizzaNotFound");
            }
        }
    }
}