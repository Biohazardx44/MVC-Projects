using BurgerApp.DataAccess.Data;
using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.Domain.Models;

namespace BurgerApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation
{
    public class BurgerRepositoryEntity : IRepository<Burger>
    {
        private BurgerAppDbContext _burgerAppDbContext;

        public BurgerRepositoryEntity(BurgerAppDbContext burgerAppDbContext)
        {
            _burgerAppDbContext = burgerAppDbContext;
        }

        /// <summary>
        /// Deletes a burger with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the burger to delete.</param>
        public void DeleteById(int id)
        {
            Burger burgerDb = _burgerAppDbContext.Burgers.FirstOrDefault(x => x.Id == id);

            if (burgerDb == null)
            {
                throw new Exception($"The burger with id {id} was not found!");
            }

            _burgerAppDbContext.Burgers.Remove(burgerDb);
            _burgerAppDbContext.SaveChanges();
        }

        /// <summary>
        /// Gets a list of all burgers from the database.
        /// </summary>
        /// <returns>A list of all burgers.</returns>
        public List<Burger> GetAll()
        {
            return _burgerAppDbContext.Burgers.ToList();
        }

        /// <summary>
        /// Gets a burger by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the burger to retrieve.</param>
        /// <returns>The burger with the specified ID, or null if not found.</returns>
        public Burger GetById(int id)
        {
            return _burgerAppDbContext.Burgers.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Inserts a new burger into the database.
        /// </summary>
        /// <param name="entity">The burger to insert.</param>
        /// <returns>The ID of the newly inserted burger.</returns>
        public int Insert(Burger entity)
        {
            _burgerAppDbContext.Burgers.Add(entity);
            return _burgerAppDbContext.SaveChanges();
        }

        /// <summary>
        /// Updates an existing burger in the database.
        /// </summary>
        /// <param name="entity">The burger to update.</param>
        public void Update(Burger entity)
        {
            _burgerAppDbContext.Burgers.Update(entity);
            _burgerAppDbContext.SaveChanges();
        }
    }
}