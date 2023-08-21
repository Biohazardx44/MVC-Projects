using Microsoft.AspNetCore.Mvc;
using PizzaApp.Services.Abstraction;
using PizzaApp.ViewModels.OrderViewModels;
using PizzaApp.ViewModels.UserViewModels;

namespace PizzaApp.Web.Controllers
{
    public class UserController : Controller
    {
        private IOrderService _orderService;
        private IUserService _userService;

        public UserController(IOrderService orderService,
                              IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for the User list page.
        /// Retrieves all users from the service and displays them in the "Index" view.
        /// </summary>
        /// <returns>The "Index" view with the list of users.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            List<UserListViewModel> userListViewModels = _userService.GetAllUsers();

            ViewData["userId"] = null;
            return View(userListViewModels);
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for displaying details of a user.
        /// Retrieves user details using the provided user ID and returns the "Details" view.
        /// If the user ID is null, the "BadRequest" view is returned. If an exception occurs,
        /// the method catches it and returns the "ExceptionPage" view.
        /// </summary>
        /// <param name="id">The ID of the user to display details for.</param>
        /// <returns>The "Details" view displaying user details, or respective error views.</returns>
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }
            try
            {
                UserDetailsViewModel userDetailsViewModel = _userService.GetUserDetails(id.Value);

                ViewData["userId"] = userDetailsViewModel.Id;
                return View(userDetailsViewModel);
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for creating a new user.
        /// Returns the "Create" view for creating a new user.
        /// </summary>
        /// <returns>The "Create" view.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            UserViewModel userViewModel = new UserViewModel();
            return View(userViewModel);
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for creating a new user.
        /// It adds the new user to the service and redirects to the "Index" view on success.
        /// If the model state is invalid, returns the "Create" view with validation errors.
        /// </summary>
        /// <param name="userViewModel">The user view model containing data for the new user.</param>
        /// <returns>Redirection to "Index" on success, or "Create" view with errors on failure.</returns>
        [HttpPost]
        public IActionResult Create(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            try
            {
                _userService.AddUser(userViewModel);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for editing a user.
        /// Retrieves user details based on the provided ID and returns the "Edit" view with user data.
        /// If the provided ID is null, returns the "BadRequest" view.
        /// If the user with the specified ID is not found, returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="id">The ID of the user to be edited.</param>
        /// <returns>
        /// The "Edit" view with user data if found.
        /// The "BadRequest" view if the provided ID is null.
        /// The "ResourceNotFound" view if the user with the specified ID is not found.
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
                UserViewModel userViewModel = _userService.GetUserForEditing(id.Value);
                return View(userViewModel);
            }
            catch (Exception e)
            {
                return View("ResourceNotFound");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for editing a user.
        /// Receives the edited user data from the "Edit" view and updates the user in the database.
        /// If the model state is not valid, returns the "Edit" view with the entered data.
        /// If the update is successful, redirects to the "Index" view.
        /// If the user with the specified ID is not found, returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="userViewModel">The edited user data from the "Edit" view.</param>
        /// <returns>
        /// If successful, redirects to the "Index" view.
        /// If model state is invalid, returns the "Edit" view with entered data.
        /// If the user with the specified ID is not found, returns the "ResourceNotFound" view.
        /// </returns>
        [HttpPost]
        public IActionResult Edit(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            try
            {
                _userService.EditUser(userViewModel);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ResourceNotFound");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for deleting a user.
        /// Retrieves user details based on the provided ID and returns the "Delete" view with user data.
        /// If the provided ID is null, returns the "BadRequest" view.
        /// If the user with the specified ID is not found, returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>
        /// The "Delete" view with user data if found.
        /// The "BadRequest" view if the provided ID is null.
        /// The "ResourceNotFound" view if the user with the specified ID is not found.
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
                UserDetailsViewModel userDetailsViewModel = _userService.GetUserDetails(id.Value);

                ViewData["userId"] = userDetailsViewModel.Id;
                return View(userDetailsViewModel);
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for deleting a user.
        /// Receives user data from the "Delete" view and deletes the user from the database.
        /// If the deletion is successful, redirects to the "Index" view.
        /// If the user with the specified ID is not found, returns the "ResourceNotFound" view.
        /// If an error occurs during the deletion process, returns the "ExceptionPage" view.
        /// </summary>
        /// <param name="userViewModel">The user data from the "Delete" view.</param>
        /// <returns>
        /// If successful, redirects to the "Index" view.
        /// If the user with the specified ID is not found, returns the "ResourceNotFound" view.
        /// If an error occurs, returns the "ExceptionPage" view.
        /// </returns>
        [HttpPost]
        public IActionResult Delete(UserDetailsViewModel userDetailsViewModel)
        {
            try
            {
                _userService.DeleteUser(userDetailsViewModel.Id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for displaying orders associated with a specific user.
        /// Retrieves a list of orders for the given user ID and displays them in the "OrdersForUser" view.
        /// If the retrieval is successful, returns the "OrdersForUser" view with the list of orders.
        /// If an error occurs during the retrieval process, returns the "ExceptionPage" view.
        /// </summary>
        /// <param name="userId">The ID of the user for whom to retrieve orders.</param>
        /// <returns>
        /// If successful, returns the "OrdersForUser" view with the list of orders.
        /// If an error occurs, returns the "ExceptionPage" view.
        /// </returns>
        [HttpGet]
        public IActionResult OrdersForUser(int userId)
        {
            try
            {
                List<OrderViewModel> orders = _orderService.GetOrdersForUser(userId);

                ViewBag.UserId = userId;
                return View("OrdersForUser", orders);
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }
    }
}