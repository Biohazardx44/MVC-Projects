using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Models;
using PizzaApp.Mappers.Extensions;
using PizzaApp.Services.Abstraction;
using PizzaApp.ViewModels.PizzaViewModels;

namespace PizzaApp.Services
{
    public class PizzaService : IPizzaService
    {
        private IPizzaRepository _pizzaRepository;

        public PizzaService(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        /// <summary>
        /// Retrieves a list of pizza view models for use in dropdowns.
        /// </summary>
        /// <returns>A list of pizza view models.</returns>
        public List<PizzaViewModel> GetPizzasForDropdown()
        {
            List<Pizza> pizzasDb = _pizzaRepository.GetAll();
            return pizzasDb.Select(x => x.MapToPizzaViewModel()).ToList();
        }

        /// <summary>
        /// Retrieves the names of pizzas currently on promotion.
        /// </summary>
        /// <returns>A comma-separated string containing the names of promotion pizzas,
        /// or a message indicating no promotion pizzas are found.</returns>
        public string GetPizzaNamesOnPromotion()
        {
            List<Pizza> promotionPizzas = _pizzaRepository.GetAll().Where(x => x.IsOnPromotion).ToList();
            if (promotionPizzas.Count > 0)
            {
                string pizzaNames = string.Join(", ", promotionPizzas.Select(x => x.Name));
                return pizzaNames;
            }

            return "No pizzas currently on promotion :(";
        }
    }
}