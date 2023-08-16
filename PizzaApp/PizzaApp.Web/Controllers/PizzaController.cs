using Microsoft.AspNetCore.Mvc;
using PizzaApp.Mappers.Extensions;
using PizzaApp.Services;
using PizzaApp.Services.Abstraction;
using PizzaApp.ViewModels.OrderViewModels;
using PizzaApp.ViewModels.PizzaViewModels;

namespace PizzaApp.Web.Controllers
{
    public class PizzaController : Controller
    {
        private IPizzaService _pizzaService;

        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for the Pizza list page.
        /// It retrieves all pizzas from the service and displays them in the "Index" view.
        /// </summary>
        /// <returns>The "Index" view with the list of pizzas.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            List<PizzaListViewModel> pizzaListViewModels = _pizzaService.GetAllPizzas();
            return View(pizzaListViewModels);
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for displaying details of a pizza order.
        /// It retrieves pizza order details using the provided order ID and returns the "Details" view.
        /// If the order ID is null, the "BadRequest" view is returned. If an exception occurs,
        /// the method catches it, and returns the "ExceptionPage" view.
        /// </summary>
        /// <param name="id">The ID of the order to display pizza order details for.</param>
        /// <returns>The "Details" view displaying pizza order details, or respective error views.</returns>
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }
            try
            {
                PizzaDetailsViewModel pizzaDetailsViewModel = _pizzaService.GetPizzaDetails(id.Value);
                return View(pizzaDetailsViewModel);
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for creating a new pizza.
        /// It returns the "Create" view for creating a new pizza.
        /// </summary>
        /// <returns>The "Create" view.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            PizzaViewModel pizzaViewModel = new PizzaViewModel();
            return View(pizzaViewModel);
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for creating a new pizza.
        /// It adds the new pizza to the service and redirects to the "Index" view on success.
        /// If the model state is invalid, it returns the "Create" view with validation errors.
        /// </summary>
        /// <param name="pizzaViewModel">The pizza view model containing the data for the new pizza.</param>
        /// <returns>Redirection to "Index" on success, or "Create" view with errors on failure.</returns>
        [HttpPost]
        public IActionResult Create(PizzaViewModel pizzaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(pizzaViewModel);
            }

            try
            {
                _pizzaService.AddPizza(pizzaViewModel);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for editing a pizza.
        /// It retrieves the pizza details based on the provided ID and returns the "Edit" view with the pizza data.
        /// If the provided ID is null, it returns the "BadRequest" view.
        /// If the pizza with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="id">The ID of the pizza to be edited.</param>
        /// <returns>
        /// The "Edit" view with the pizza data if found.
        /// The "BadRequest" view if the provided ID is null.
        /// The "ResourceNotFound" view if the pizza with the specified ID is not found.
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
                PizzaViewModel pizzaViewModel = _pizzaService.GetPizzaForEditing(id.Value);
                return View(pizzaViewModel);
            }
            catch (Exception e)
            {
                return View("ResourceNotFound");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for editing a pizza.
        /// It receives the edited pizza data from the "Edit" view and updates the pizza in the database.
        /// If the model state is not valid, it returns the "Edit" view with the entered data.
        /// If the update is successful, it redirects to the "Index" view.
        /// If the pizza with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="pizzaViewModel">The edited pizza data from the "Edit" view.</param>
        /// <returns>
        /// If successful, redirects to the "Index" view.
        /// If model state is invalid, returns the "Edit" view with entered data.
        /// If the pizza with the specified ID is not found, returns the "ResourceNotFound" view.
        /// </returns>
        [HttpPost]
        public IActionResult Edit(PizzaViewModel pizzaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(pizzaViewModel);
            }

            try
            {
                _pizzaService.EditPizza(pizzaViewModel);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ResourceNotFound");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for deleting a pizza.
        /// It retrieves the pizza details based on the provided ID and returns the "Delete" view with the pizza data.
        /// If the provided ID is null, it returns the "BadRequest" view.
        /// If the pizza with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="id">The ID of the pizza to be deleted.</param>
        /// <returns>
        /// The "Delete" view with the pizza data if found.
        /// The "BadRequest" view if the provided ID is null.
        /// The "ResourceNotFound" view if the pizza with the specified ID is not found.
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
                PizzaDetailsViewModel pizzaDetailsViewModel = _pizzaService.GetPizzaDetails(id.Value);
                PizzaViewModel pizzaViewModel = pizzaDetailsViewModel.MapFromPizzaDetailsViewModel();

                return View(pizzaViewModel);
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for deleting a pizza.
        /// It receives the pizza data from the "Delete" view and deletes the pizza from the database.
        /// If the deletion is successful, it redirects to the "Index" view.
        /// If the pizza with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// If an error occurs during the deletion process, it returns the "ExceptionPage" view.
        /// </summary>
        /// <param name="pizzaViewModel">The pizza data from the "Delete" view.</param>
        /// <returns>
        /// If successful, redirects to the "Index" view.
        /// If the pizza with the specified ID is not found, returns the "ResourceNotFound" view.
        /// If an error occurs, returns the "ExceptionPage" view.
        /// </returns>
        [HttpPost]
        public IActionResult Delete(PizzaViewModel pizzaViewModel)
        {
            try
            {
                _pizzaService.DeletePizza(pizzaViewModel.Id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }
    }
}