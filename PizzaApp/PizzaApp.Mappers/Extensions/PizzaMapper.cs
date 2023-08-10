using PizzaApp.Domain.Models;
using PizzaApp.ViewModels.PizzaViewModels;

namespace PizzaApp.Mappers.Extensions
{
    public static class PizzaMapper
    {
        public static PizzaViewModel MapToPizzaViewModel(this Pizza pizza)
        {
            return new PizzaViewModel
            {
                Id = pizza.Id,
                Name = pizza.Name
            };
        }
    }
}