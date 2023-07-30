using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.Domain.Models;
using BurgerApp.Mappers.Extensions;
using BurgerApp.Services.Abstraction;
using BurgerApp.ViewModels.BurgerViewModels;

namespace BurgerApp.Services
{
    public class BurgerService : IBurgerService
    {
        private IRepository<Order> _orderRepository;
        private IRepository<Burger> _burgerRepository;

        public BurgerService(IRepository<Order> orderRepository,
                             IRepository<Burger> burgerRepository)
        {
            _orderRepository = orderRepository;
            _burgerRepository = burgerRepository;
        }

        /// <summary>
        /// Adds a new burger to the database.
        /// </summary>
        /// <param name="burgerViewModel">The view model containing the burger details.</param>
        public void AddBurger(BurgerViewModel burgerViewModel)
        {
            Burger burger = new Burger();

            burgerViewModel.MapToBurger(burger);

            int newBurgerId = _burgerRepository.Insert(burger);
            if (newBurgerId <= 0)
            {
                throw new Exception("An error occurred while saving to the database!");
            }
        }

        /// <summary>
        /// Deletes a burger from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the burger to be deleted.</param>
        public void DeleteBurger(int id)
        {
            Burger burgerDb = _burgerRepository.GetById(id);
            if (burgerDb == null)
            {
                throw new Exception($"The burger with id {id} was not found!");
            }
            _burgerRepository.DeleteById(id);
        }

        /// <summary>
        /// Updates an existing burger in the database with the new details.
        /// </summary>
        /// <param name="burgerViewModel">The view model containing the updated burger details.</param>
        public void EditBurger(BurgerViewModel burgerViewModel)
        {
            Burger burgerDb = _burgerRepository.GetById(burgerViewModel.Id);
            if (burgerDb == null)
            {
                throw new Exception($"The burger with id {burgerViewModel.Id} was not found!");
            }

            burgerViewModel.MapToBurger(burgerDb);
            burgerDb.Id = burgerViewModel.Id;
            _burgerRepository.Update(burgerDb);
        }

        /// <summary>
        /// Retrieves a list of all burgers from the database.
        /// </summary>
        /// <returns>A list of view models representing all burgers.</returns>
        public List<BurgerListViewModel> GetAllBurgers()
        {
            List<Burger> dbBurgers = _burgerRepository.GetAll();
            return dbBurgers.Select(x => x.MapToBurgerListViewModel()).ToList();
        }

        /// <summary>
        /// Retrieves the details of a specific burger from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the burger to retrieve.</param>
        /// <returns>The view model representing the details of the specified burger.</returns>
        public BurgerViewModel GetBurgerDetails(int id)
        {
            Burger burgerDb = _burgerRepository.GetById(id);
            if (burgerDb == null)
            {
                throw new Exception($"The burger with id {id} was not found!");
            }
            return burgerDb.MapToBurgerViewModel();
        }

        /// <summary>
        /// Retrieves a burger view model for editing based on its ID.
        /// </summary>
        /// <param name="id">The ID of the burger to retrieve for editing.</param>
        /// <returns>The view model representing the burger details for editing.</returns>
        public BurgerViewModel GetBurgerForEditing(int id)
        {
            Burger burgerDb = _burgerRepository.GetById(id);
            if (burgerDb == null)
            {
                throw new Exception($"The burger with id {id} was not found!");
            }

            return burgerDb.MapToBurgerViewModel();
        }

        /// <summary>
        /// Retrieves a list of burger view models for use in dropdowns.
        /// </summary>
        /// <returns>A list of burger view models.</returns>
        public List<BurgerViewModel> GetBurgersForDropdown()
        {
            List<Burger> burgerDb = _burgerRepository.GetAll();
            return burgerDb.Select(x => x.MapToBurgerViewModel()).ToList();
        }

        /// <summary>
        /// Retrieves a list of the most popular burgers based on the number of times they have been ordered.
        /// </summary>
        /// <returns>A list of the most popular burger view models.</returns>
        public List<BurgerViewModel> GetMostPopularBurgers()
        {
            List<Order> orders = _orderRepository.GetAll();
            Dictionary<int, int> burgerCountMap = new Dictionary<int, int>();

            foreach (var order in orders)
            {
                foreach (var burgerOrder in order.BurgerOrders)
                {
                    int burgerId = burgerOrder.BurgerId;
                    if (burgerCountMap.ContainsKey(burgerId))
                    {
                        burgerCountMap[burgerId]++;
                    }
                    else
                    {
                        burgerCountMap[burgerId] = 1;
                    }
                }
            }

            if (burgerCountMap.Count == 0)
            {
                return new List<BurgerViewModel>();
            }

            int maxCount = burgerCountMap.Values.Max();

            List<BurgerViewModel> mostPopularBurgers = burgerCountMap
                .Where(kvp => kvp.Value == maxCount)
                .Select(kvp => _burgerRepository.GetById(kvp.Key).MapToBurgerViewModel())
                .ToList();

            return mostPopularBurgers;
        }
    }
}