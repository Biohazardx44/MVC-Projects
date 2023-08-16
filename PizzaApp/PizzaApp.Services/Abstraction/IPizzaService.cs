using PizzaApp.ViewModels.PizzaViewModels;

namespace PizzaApp.Services.Abstraction
{
    public interface IPizzaService
    {
        List<PizzaListViewModel> GetAllPizzas();
        PizzaDetailsViewModel GetPizzaDetails(int id);
        void AddPizza(PizzaViewModel pizzaViewModel);
        PizzaViewModel GetPizzaForEditing(int id);
        void EditPizza(PizzaViewModel pizzaViewModel);
        void DeletePizza(int id);
        List<PizzaViewModel> GetPizzasForDropdown();
        List<string> GetPizzaNamesOnPromotion();
    }
}