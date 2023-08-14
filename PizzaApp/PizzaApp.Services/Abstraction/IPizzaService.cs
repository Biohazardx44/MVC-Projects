using PizzaApp.ViewModels.PizzaViewModels;

namespace PizzaApp.Services.Abstraction
{
    public interface IPizzaService
    {
        List<PizzaViewModel> GetPizzasForDropdown();
        string GetPizzaNamesOnPromotion();
    }
}