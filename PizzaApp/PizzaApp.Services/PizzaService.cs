using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Models;
using PizzaApp.Mappers.Extensions;
using PizzaApp.Services.Abstraction;
using PizzaApp.ViewModels.PizzaViewModels;

namespace PizzaApp.Services
{
    public class PizzaService : IPizzaService
    {
        private IOrderRepository _orderRepository;
        private IRepository<Pizza> _pizzaRepository;

        public PizzaService(IOrderRepository orderRepository,
                            IRepository<Pizza> pizzaRepository)
        {
            _orderRepository = orderRepository;
            _pizzaRepository = pizzaRepository;
        }

        /// <summary>
        /// Adds a new pizza to the database.
        /// </summary>
        /// <param name="pizzaViewModel">The view model containing the pizza details.</param>
        public void AddPizza(PizzaViewModel pizzaViewModel)
        {
            Pizza pizza = new Pizza();
            pizzaViewModel.MapToPizza(pizza);

            int newPizzaId = _pizzaRepository.Insert(pizza);
            if (newPizzaId <= 0)
            {
                throw new Exception("An error occurred while saving to the database!");
            }
        }

        /// <summary>
        /// Deletes a pizza from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the pizza to be deleted.</param>
        public void DeletePizza(int id)
        {
            Pizza pizzaDb = _pizzaRepository.GetById(id);
            if (pizzaDb == null)
            {
                throw new Exception($"The pizza with id {id} was not found!");
            }
            _pizzaRepository.DeleteById(id);
        }

        /// <summary>
        /// Updates an existing pizza in the database with the new details.
        /// </summary>
        /// <param name="pizzaViewModel">The view model containing the updated pizza details.</param>
        public void EditPizza(PizzaViewModel pizzaViewModel)
        {
            Pizza pizzaDb = _pizzaRepository.GetById(pizzaViewModel.Id);
            if (pizzaDb == null)
            {
                throw new Exception($"The pizza with id {pizzaViewModel.Id} was not found!");
            }

            pizzaViewModel.MapToPizza(pizzaDb);
            _pizzaRepository.Update(pizzaDb);
        }

        /// <summary>
        /// Retrieves a list of all pizzas from the database.
        /// </summary>
        /// <returns>A list of view models representing all pizzas.</returns>
        public List<PizzaListViewModel> GetAllPizzas()
        {
            List<Pizza> dbPizzas = _pizzaRepository.GetAll();
            return dbPizzas.Select(pizza => pizza.MapToPizzaListViewModel()).ToList();
        }

        /// <summary>
        /// Retrieves the details of a specific pizza from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the pizza to retrieve.</param>
        /// <returns>The view model representing the details of the specified pizza.</returns>
        public PizzaDetailsViewModel GetPizzaDetails(int id)
        {
            Pizza pizzaDb = _pizzaRepository.GetById(id);
            if (pizzaDb == null)
            {
                throw new Exception($"The pizza with id {id} was not found!");
            }
            return pizzaDb.MapToPizzaDetailsViewModel();
        }

        /// <summary>
        /// Retrieves a pizza view model for editing based on its ID.
        /// </summary>
        /// <param name="id">The ID of the pizza to retrieve for editing.</param>
        /// <returns>The view model representing the pizza details for editing.</returns>
        public PizzaViewModel GetPizzaForEditing(int id)
        {
            Pizza pizzaDb = _pizzaRepository.GetById(id);
            if (pizzaDb == null)
            {
                throw new Exception($"The pizza with id {id} was not found!");
            }

            return pizzaDb.MapToPizzaViewModel();
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
        public List<string> GetPizzaNamesOnPromotion()
        {
            List<Pizza> promotionPizzas = _pizzaRepository.GetAll().Where(x => x.IsOnPromotion).ToList();
            if (promotionPizzas.Count > 0)
            {
                List<string> pizzaNames = promotionPizzas.Select(x => x.Name).ToList();
                return pizzaNames;
            }

            return new List<string>();
        }
    }
}