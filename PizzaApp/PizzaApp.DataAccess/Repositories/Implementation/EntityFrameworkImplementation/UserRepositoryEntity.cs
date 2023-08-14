using PizzaApp.DataAccess.Data;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Models;

namespace PizzaApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation
{
    public class UserRepositoryEntity : IRepository<User>
    {
        private PizzaAppDbContext _pizzaAppDbContext;

        public UserRepositoryEntity(PizzaAppDbContext pizzaAppDbContext)
        {
            _pizzaAppDbContext = pizzaAppDbContext;
        }

        /// <summary>
        /// Deletes a user with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        public void DeleteById(int id)
        {
            User userDb = _pizzaAppDbContext.Users.FirstOrDefault(user => user.Id == id);

            if (userDb == null)
            {
                throw new Exception($"The user with id {id} was not found!");
            }

            _pizzaAppDbContext.Users.Remove(userDb);
            _pizzaAppDbContext.SaveChanges();
        }

        /// <summary>
        /// Gets a list of all users from the database.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public List<User> GetAll()
        {
            return _pizzaAppDbContext.Users.ToList();
        }

        /// <summary>
        /// Gets a user by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID, or null if not found.</returns>
        public User GetById(int id)
        {
            return _pizzaAppDbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Inserts a new user into the database.
        /// </summary>
        /// <param name="entity">The user to insert.</param>
        /// <returns>The ID of the newly inserted user.</returns>
        public int Insert(User entity)
        {
            _pizzaAppDbContext.Users.Add(entity);
            return _pizzaAppDbContext.SaveChanges();
        }

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="entity">The user to update.</param>
        public void Update(User entity)
        {
            _pizzaAppDbContext.Users.Update(entity);
            _pizzaAppDbContext.SaveChanges();
        }
    }
}