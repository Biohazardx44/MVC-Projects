using PizzaApp.DataAccess.Data;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Models;

namespace PizzaApp.DataAccess.Repositories.Implementation.StaticDbImplementation
{
    public class PizzaRepository : IPizzaRepository
    {
        /// <summary>
        /// Deletes a pizza by its ID from the StaticDb.
        /// </summary>
        /// <param name="id">The ID of the pizza to delete.</param>
        /// <exception cref="Exception">Thrown when the pizza with the specified ID is not found.</exception>
        public void DeleteById(int id)
        {
            Pizza pizza = StaticDb.Pizzas.FirstOrDefault(x => x.Id == id);
            if (pizza == null)
            {
                throw new Exception($"Pizza with id {id} was not found!");
            }
            StaticDb.Pizzas.Remove(pizza);
        }

        /// <summary>
        /// Gets all pizzas from the StaticDb.
        /// </summary>
        /// <returns>A list of all pizzas in the StaticDb.</returns>
        public List<Pizza> GetAll()
        {
            return StaticDb.Pizzas;
        }

        /// <summary>
        /// Gets a pizza by its ID from the StaticDb.
        /// </summary>
        /// <param name="id">The ID of the pizza to retrieve.</param>
        /// <returns>The pizza with the specified ID if found; otherwise, null.</returns>
        public Pizza GetById(int id)
        {
            return StaticDb.Pizzas.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Inserts a new pizza into the StaticDb.
        /// </summary>
        /// <param name="entity">The pizza entity to insert.</param>
        /// <returns>The ID of the newly inserted pizza.</returns>
        public int Insert(Pizza entity)
        {
            entity.Id = ++StaticDb.PizzaId;
            StaticDb.Pizzas.Add(entity);
            return entity.Id;
        }

        /// <summary>
        /// Updates an existing pizza in the StaticDb.
        /// </summary>
        /// <param name="entity">The updated pizza entity.</param>
        /// <exception cref="Exception">Thrown when the pizza with the specified ID is not found.</exception>
        public void Update(Pizza entity)
        {
            Pizza pizza = StaticDb.Pizzas.FirstOrDefault(x => x.Id == entity.Id);
            if (pizza == null)
            {
                throw new Exception($"Pizza with id {entity.Id} was not found!");
            }
            int index = StaticDb.Pizzas.IndexOf(pizza);
            StaticDb.Pizzas[index] = entity;
        }
    }
}