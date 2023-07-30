using BurgerApp.Services.Abstraction;
using BurgerApp.ViewModels.LocationViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BurgerApp.Web.Controllers
{
    public class LocationController : Controller
    {
        private ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for the Location list page.
        /// It retrieves all locations from the service and displays them in the "Index" view.
        /// </summary>
        /// <returns>The "Index" view with the list of locations.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            List<LocationListViewModel> locationListViewModels = _locationService.GetAllLocations();
            return View(locationListViewModels);
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for creating a new location.
        /// It returns the "Create" view for creating a new location.
        /// </summary>
        /// <returns>The "Create" view.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            LocationViewModel locationViewModel = new LocationViewModel();
            return View(locationViewModel);
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for creating a new location.
        /// It adds the new location to the service and redirects to the "Index" view on success.
        /// If the model state is invalid, it returns the "Create" view with validation errors.
        /// </summary>
        /// <param name="locationViewModel">The location view model containing the data for the new location.</param>
        /// <returns>Redirection to "Index" on success, or "Create" view with errors on failure.</returns>
        [HttpPost]
        public IActionResult Create(LocationViewModel locationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(locationViewModel);
            }

            try
            {
                _locationService.AddLocation(locationViewModel);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for editing a location.
        /// It retrieves the location details based on the provided ID and returns the "Edit" view with the location data.
        /// If the provided ID is null, it returns the "BadRequest" view.
        /// If the location with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="id">The ID of the location to be edited.</param>
        /// <returns>
        /// The "Edit" view with the location data if found.
        /// The "BadRequest" view if the provided ID is null.
        /// The "ResourceNotFound" view if the location with the specified ID is not found.
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
                LocationViewModel locationViewModel = _locationService.GetLocationForEditing(id.Value);
                return View(locationViewModel);
            }
            catch (Exception e)
            {
                return View("ResourceNotFound");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for editing a location.
        /// It receives the edited location data from the "Edit" view and updates the location in the database.
        /// If the model state is not valid, it returns the "Edit" view with the entered data.
        /// If the update is successful, it redirects to the "Index" view.
        /// If the location with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="locationViewModel">The edited location data from the "Edit" view.</param>
        /// <returns>
        /// If successful, redirects to the "Index" view.
        /// If model state is invalid, returns the "Edit" view with entered data.
        /// If the location with the specified ID is not found, returns the "ResourceNotFound" view.
        /// </returns>
        [HttpPost]
        public IActionResult Edit(LocationViewModel locationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(locationViewModel);
            }

            try
            {
                _locationService.EditLocation(locationViewModel);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ResourceNotFound");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP GET request for deleting a location.
        /// It retrieves the location details based on the provided ID and returns the "Delete" view with the location data.
        /// If the provided ID is null, it returns the "BadRequest" view.
        /// If the location with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// </summary>
        /// <param name="id">The ID of the location to be deleted.</param>
        /// <returns>
        /// The "Delete" view with the location data if found.
        /// The "BadRequest" view if the provided ID is null.
        /// The "ResourceNotFound" view if the location with the specified ID is not found.
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
                LocationViewModel locationViewModel = _locationService.GetLocationDetails(id.Value);
                return View(locationViewModel);
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }

        /// <summary>
        /// Action method that handles the HTTP POST request for deleting a location.
        /// It receives the location data from the "Delete" view and deletes the location from the database.
        /// If the deletion is successful, it redirects to the "Index" view.
        /// If the location with the specified ID is not found, it returns the "ResourceNotFound" view.
        /// If an error occurs during the deletion process, it returns the "ExceptionPage" view.
        /// </summary>
        /// <param name="locationViewModel">The location data from the "Delete" view.</param>
        /// <returns>
        /// If successful, redirects to the "Index" view.
        /// If the location with the specified ID is not found, returns the "ResourceNotFound" view.
        /// If an error occurs, returns the "ExceptionPage" view.
        /// </returns>
        [HttpPost]
        public IActionResult Delete(LocationViewModel locationViewModel)
        {
            try
            {
                _locationService.DeleteLocation(locationViewModel.Id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ExceptionPage");
            }
        }
    }
}