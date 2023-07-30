using BurgerApp.Services.Abstraction;
using BurgerApp.ViewModels.BurgerViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BurgerApp.Web.Controllers
{
    public class BurgerController : Controller
    {
        private IBurgerService _burgerService;

        public BurgerController(IBurgerService burgerService)
        {
            _burgerService = burgerService;
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for the Burger list page.
        /// It retrieves all burgers from the service and displays them in the "Index" view.
        /// </summary>
        /// <returns>The "Index" view with the list of burgers.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            List<BurgerListViewModel> burgerListViewModels = _burgerService.GetAllBurgers();
            return View(burgerListViewModels);
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for creating a new burger.
        /// It returns the "Create" view for creating a new burger.
        /// </summary>
        /// <returns>The "Create" view.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            BurgerViewModel burgerViewModel = new BurgerViewModel();
            return View(burgerViewModel);
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for creating a new burger.
        /// It adds the new burger to the service and redirects to the "Index" view on success.
        /// If the model state is invalid, it returns the "Create" view with validation errors.
        /// </summary>
        /// <param name="burgerViewModel">The burger view model containing the data for the new burger.</param>
        /// <returns>Redirection to "Index" on success, or "Create" view with errors on failure.</returns>
        [HttpPost]
        public IActionResult Create(BurgerViewModel burgerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(burgerViewModel);
            }

            try
            {
                _burgerService.AddBurger(burgerViewModel);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for editing a burger.
        /// It retrieves the burger details based on the provided ID and returns the "Edit" view with the burger data.
        /// If the provided ID is null, it returns the "BadRequest" view.
        /// If the burger with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="id">The ID of the burger to be edited.</param>
        /// <returns>
        /// The "Edit" view with the burger data if found.
        /// The "BadRequest" view if the provided ID is null.
        /// The "ResourceNotFound" view if the burger with the specified ID is not found.
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
                BurgerViewModel burgerViewModel = _burgerService.GetBurgerForEditing(id.Value);
                return View(burgerViewModel);
            }
            catch (Exception e)
            {
                return View("ResourceNotFound");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for editing a burger.
        /// It receives the edited burger data from the "Edit" view and updates the burger in the database.
        /// If the model state is not valid, it returns the "Edit" view with the entered data.
        /// If the update is successful, it redirects to the "Index" view.
        /// If the burger with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="burgerViewModel">The edited burger data from the "Edit" view.</param>
        /// <returns>
        /// If successful, redirects to the "Index" view.
        /// If model state is invalid, returns the "Edit" view with entered data.
        /// If the burger with the specified ID is not found, returns the "ResourceNotFound" view.
        /// </returns>
        [HttpPost]
        public IActionResult Edit(BurgerViewModel burgerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(burgerViewModel);
            }

            try
            {
                _burgerService.EditBurger(burgerViewModel);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ResourceNotFound");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for deleting a burger.
        /// It retrieves the burger details based on the provided ID and returns the "Delete" view with the burger data.
        /// If the provided ID is null, it returns the "BadRequest" view.
        /// If the burger with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="id">The ID of the burger to be deleted.</param>
        /// <returns>
        /// The "Delete" view with the burger data if found.
        /// The "BadRequest" view if the provided ID is null.
        /// The "ResourceNotFound" view if the burger with the specified ID is not found.
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
                BurgerViewModel burgerViewModel = _burgerService.GetBurgerDetails(id.Value);
                return View(burgerViewModel);
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for deleting a burger.
        /// It receives the burger data from the "Delete" view and deletes the burger from the database.
        /// If the deletion is successful, it redirects to the "Index" view.
        /// If the burger with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// If an error occurs during the deletion process, it returns the "ExceptionPage" view.
        /// </summary>
        /// <param name="burgerViewModel">The burger data from the "Delete" view.</param>
        /// <returns>
        /// If successful, redirects to the "Index" view.
        /// If the burger with the specified ID is not found, returns the "ResourceNotFound" view.
        /// If an error occurs, returns the "ExceptionPage" view.
        /// </returns>
        [HttpPost]
        public IActionResult Delete(BurgerViewModel burgerViewModel)
        {
            try
            {
                _burgerService.DeleteBurger(burgerViewModel.Id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }
    }
}