using PizzaApp.DataAccess.Data;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Models;

namespace PizzaApp.DataAccess.Repositories.Implementation.StaticDbImplementation
{
    public class UserRepository : IRepository<User>
    {
        /// <summary>
        /// Deletes a user by its ID from the StaticDb.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <exception cref="Exception">Thrown when the user with the specified ID is not found.</exception>
        public void DeleteById(int id)
        {
            User user = StaticDb.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                throw new Exception($"User with id {id} was not found!");
            }
            StaticDb.Users.Remove(user);
        }

        /// <summary>
        /// Gets all users from the StaticDb.
        /// </summary>
        /// <returns>A list of all users in the StaticDb.</returns>
        public List<User> GetAll()
        {
            return StaticDb.Users;
        }

        /// <summary>
        /// Gets a user by its ID from the StaticDb.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID if found; otherwise, null.</returns>
        public User GetById(int id)
        {
            return StaticDb.Users.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Inserts a new user into the StaticDb.
        /// </summary>
        /// <param name="entity">The user entity to insert.</param>
        /// <returns>The ID of the newly inserted user.</returns>
        public int Insert(User entity)
        {
            entity.Id = ++StaticDb.UserId;
            StaticDb.Users.Add(entity);
            return entity.Id;
        }

        /// <summary>
        /// Updates an existing user in the StaticDb.
        /// </summary>
        /// <param name="entity">The updated user entity.</param>
        /// <exception cref="Exception">Thrown when the user with the specified ID is not found.</exception>
        public void Update(User entity)
        {
            User user = StaticDb.Users.FirstOrDefault(x => x.Id == entity.Id);
            if (user == null)
            {
                throw new Exception($"User with id {entity.Id} was not found!");
            }
            int index = StaticDb.Users.IndexOf(user);
            StaticDb.Users[index] = entity;
        }
    }
}