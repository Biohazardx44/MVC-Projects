using BurgerApp.Domain.Models;
using BurgerApp.ViewModels.BurgerViewModels;

namespace BurgerApp.Mappers.Extensions
{
    public static class BurgerMapper
    {
        public static void MapToBurger(this BurgerViewModel burgerViewModel, Burger burger)
        {
            burger.Name = burgerViewModel.Name;
            burger.Price = burgerViewModel.Price;
            burger.IsVegetarian = burgerViewModel.IsVegetarian;
            burger.IsVegan = burgerViewModel.IsVegan;
            burger.HasFries = burgerViewModel.HasFries;
            burger.BurgerSize = burgerViewModel.BurgerSize;
        }

        public static BurgerViewModel MapToBurgerViewModel(this Burger burger)
        {
            if (burger == null)
            {
                return new BurgerViewModel();
            }

            return new BurgerViewModel
            {
                Id = burger.Id,
                Name = burger.Name,
                Price = burger.Price,
                IsVegetarian = burger.IsVegetarian,
                IsVegan = burger.IsVegan,
                HasFries = burger.HasFries,
                BurgerSize = burger.BurgerSize,
            };
        }

        public static BurgerListViewModel MapToBurgerListViewModel(this Burger burger)
        {
            return new BurgerListViewModel
            {
                Id = burger.Id,
                Name = burger.Name,
                Price = burger.Price,
                IsVegetarian = burger.IsVegetarian,
                IsVegan = burger.IsVegan,
                HasFries = burger.HasFries,
                BurgerSize = burger.BurgerSize,
            };
        }
    }
}