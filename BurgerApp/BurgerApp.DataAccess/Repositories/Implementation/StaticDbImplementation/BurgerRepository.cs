using BurgerApp.DataAccess.Data;
using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.Domain.Models;

namespace BurgerApp.DataAccess.Repositories.Implementation.StaticDbImplementation
{
    public class BurgerRepository : IBurgerRepository
    {
        /// <summary>
        /// Deletes a burger by its ID from the StaticDb.
        /// </summary>
        /// <param name="id">The ID of the burger to delete.</param>
        /// <exception cref="Exception">Thrown when the burger with the specified ID is not found.</exception>
        public void DeleteById(int id)
        {
            Burger burger = StaticDb.Burgers.FirstOrDefault(x => x.Id == id);
            if (burger == null)
            {
                throw new Exception($"Burger with id {id} was not found!");
            }
            StaticDb.Burgers.Remove(burger);
        }

        /// <summary>
        /// Gets all burgers from the StaticDb.
        /// </summary>
        /// <returns>A list of all burgers in the StaticDb.</returns>
        public List<Burger> GetAll()
        {
            return StaticDb.Burgers;
        }

        /// <summary>
        /// Gets a burger by its ID from the StaticDb.
        /// </summary>
        /// <param name="id">The ID of the burger to retrieve.</param>
        /// <returns>The burger with the specified ID if found; otherwise, null.</returns>
        public Burger GetById(int id)
        {
            return StaticDb.Burgers.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Inserts a new burger into the StaticDb.
        /// </summary>
        /// <param name="entity">The burger entity to insert.</param>
        /// <returns>The ID of the newly inserted burger.</returns>
        public int Insert(Burger entity)
        {
            entity.Id = ++StaticDb.BurgerId;
            StaticDb.Burgers.Add(entity);
            return entity.Id;
        }

        /// <summary>
        /// Updates an existing burger in the StaticDb.
        /// </summary>
        /// <param name="entity">The updated burger entity.</param>
        /// <exception cref="Exception">Thrown when the burger with the specified ID is not found.</exception>
        public void Update(Burger entity)
        {
            Burger burger = StaticDb.Burgers.FirstOrDefault(x => x.Id == entity.Id);
            if (burger == null)
            {
                throw new Exception($"Burger with id {entity.Id} was not found!");
            }
            int index = StaticDb.Burgers.IndexOf(burger);
            StaticDb.Burgers[index] = entity;
        }
    }
}