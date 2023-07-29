using BurgerApp.ViewModels.BurgerViewModels;

namespace BurgerApp.Services.Abstraction
{
    public interface IBurgerService
    {
        List<BurgerListViewModel> GetAllBurgers();
        BurgerViewModel GetBurgerDetails(int id);
        void AddBurger(BurgerViewModel burgerViewModel);
        BurgerViewModel GetBurgerForEditing(int id);
        void EditBurger(BurgerViewModel burgerViewModel);
        void DeleteBurger(int id);
        List<BurgerViewModel> GetBurgersForDropdown();
        List<BurgerViewModel> GetMostPopularBurgers();
    }
}