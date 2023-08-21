using PizzaApp.Domain.Models;
using PizzaApp.ViewModels.PizzaViewModels;

namespace PizzaApp.Mappers.Extensions
{
    public static class PizzaMapper
    {
        public static void MapToPizza(this PizzaViewModel pizzaViewModel, Pizza pizza)
        {
            pizza.Id = pizzaViewModel.Id;
            pizza.Name = pizzaViewModel.Name;
            pizza.IsOnPromotion = pizzaViewModel.IsOnPromotion;
            pizza.PizzaSize = pizzaViewModel.PizzaSize;
        }

        public static PizzaViewModel MapToPizzaViewModel(this Pizza pizza)
        {
            if (pizza == null)
            {
                return new PizzaViewModel();
            }

            return new PizzaViewModel
            {
                Id = pizza.Id,
                Name = pizza.Name,
                IsOnPromotion = pizza.IsOnPromotion,
                PizzaSize = pizza.PizzaSize,
            };
        }

        public static PizzaListViewModel MapToPizzaListViewModel(this Pizza pizza)
        {
            return new PizzaListViewModel
            {
                Id = pizza.Id,
                Name = pizza.Name,
                PizzaSize = pizza.PizzaSize
            };
        }

        public static PizzaDetailsViewModel MapToPizzaDetailsViewModel(this Pizza pizza)
        {
            return new PizzaDetailsViewModel
            {
                Id = pizza.Id,
                Name = pizza.Name,
                IsOnPromotion = pizza.IsOnPromotion,
                PizzaSize = pizza.PizzaSize,
            };
        }
    }
}