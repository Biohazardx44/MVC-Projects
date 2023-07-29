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

        [HttpGet]
        public IActionResult Index()
        {
            List<LocationListViewModel> locationListViewModels = _locationService.GetAllLocations();
            return View(locationListViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LocationViewModel locationViewModel = new LocationViewModel();
            return View(locationViewModel);
        }

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