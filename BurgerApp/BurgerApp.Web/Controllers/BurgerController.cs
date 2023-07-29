using BurgerApp.Services.Abstraction;
using BurgerApp.ViewModels.BurgerViewModels;
using BurgerApp.ViewModels.LocationViewModels;
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

        [HttpGet]
        public IActionResult Index()
        {
            List<BurgerListViewModel> burgerListViewModels = _burgerService.GetAllBurgers();
            return View(burgerListViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            BurgerViewModel burgerViewModel = new BurgerViewModel();
            return View(burgerViewModel);
        }

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