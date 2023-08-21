using PizzaApp.DataAccess.Data;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Models;

namespace PizzaApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation
{
    public class PizzaRepositoryEntity : IRepository<Pizza>
    {
        private PizzaAppDbContext _pizzaAppDbContext;

        public PizzaRepositoryEntity(PizzaAppDbContext pizzaAppDbContext)
        {
            _pizzaAppDbContext = pizzaAppDbContext;
        }

        /// <summary>
        /// Deletes a pizza with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the pizza to delete.</param>
        public void DeleteById(int id)
        {
            Pizza pizzaDb = _pizzaAppDbContext.Pizzas.FirstOrDefault(pizza => pizza.Id == id);

            if (pizzaDb == null)
            {
                throw new Exception($"The pizza with id {id} was not found!");
            }

            _pizzaAppDbContext.Pizzas.Remove(pizzaDb);
            _pizzaAppDbContext.SaveChanges();
        }

        /// <summary>
        /// Gets a list of all pizzas from the database.
        /// </summary>
        /// <returns>A list of all pizzas.</returns>
        public List<Pizza> GetAll()
        {
            return _pizzaAppDbContext.Pizzas.ToList();
        }

        /// <summary>
        /// Gets a pizza by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the pizza to retrieve.</param>
        /// <returns>The pizza with the specified ID, or null if not found.</returns>
        public Pizza GetById(int id)
        {
            return _pizzaAppDbContext.Pizzas.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Inserts a new pizza into the database.
        /// </summary>
        /// <param name="entity">The pizza to insert.</param>
        /// <returns>The ID of the newly inserted pizza.</returns>
        public int Insert(Pizza entity)
        {
            _pizzaAppDbContext.Pizzas.Add(entity);
            return _pizzaAppDbContext.SaveChanges();
        }

        /// <summary>
        /// Updates an existing pizza in the database.
        /// </summary>
        /// <param name="entity">The pizza to update.</param>
        public void Update(Pizza entity)
        {
            _pizzaAppDbContext.Pizzas.Update(entity);
            _pizzaAppDbContext.SaveChanges();
        }
    }
}