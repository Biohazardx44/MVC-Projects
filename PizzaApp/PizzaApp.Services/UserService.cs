using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Models;
using PizzaApp.Mappers.Extensions;
using PizzaApp.Services.Abstraction;
using PizzaApp.ViewModels.UserViewModels;

namespace PizzaApp.Services
{
    public class UserService : IUserService
    {
        private IOrderRepository _orderRepository;
        private IUserRepository _userRepository;

        public UserService(IOrderRepository orderRepository,
                           IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="userViewModel">The view model containing the user details.</param>
        public void AddUser(UserViewModel userViewModel)
        {
            User user = new User();
            userViewModel.MapToUser(user);

            int newUserId = _userRepository.Insert(user);
            if (newUserId <= 0)
            {
                throw new Exception("An error occurred while saving to the database!");
            }
        }

        /// <summary>
        /// Deletes a user from the database based on their ID.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        public void DeleteUser(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new Exception($"The user with id {id} was not found!");
            }
            _userRepository.DeleteById(id);
        }

        /// <summary>
        /// Updates an existing user in the database with the new details.
        /// </summary>
        /// <param name="userViewModel">The view model containing the updated user details.</param>
        public void EditUser(UserViewModel userViewModel)
        {
            User userDb = _userRepository.GetById(userViewModel.Id);
            if (userDb == null)
            {
                throw new Exception($"The user with id {userViewModel.Id} was not found!");
            }

            userViewModel.MapToUser(userDb);
            _userRepository.Update(userDb);
        }

        /// <summary>
        /// Retrieves a list of all users from the database.
        /// </summary>
        /// <returns>A list of view models representing all users.</returns>
        public List<UserListViewModel> GetAllUsers()
        {
            List<User> dbUsers = _userRepository.GetAll();
            return dbUsers.Select(user => user.MapToUserListViewModel()).ToList();
        }

        /// <summary>
        /// Retrieves the details of a specific user from the database based on their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The view model representing the details of the specified user.</returns>
        public UserDetailsViewModel GetUserDetails(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new Exception($"The user with id {id} was not found!");
            }
            return userDb.MapToUserDetailsViewModel();
        }

        /// <summary>
        /// Retrieves a user view model for editing based on their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve for editing.</param>
        /// <returns>The view model representing the user details for editing.</returns>
        public UserViewModel GetUserForEditing(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new Exception($"The user with id {id} was not found!");
            }

            return userDb.MapToUserViewModel();
        }

        /// <summary>
        /// Retrieves a list of user view models for use in dropdowns.
        /// </summary>
        /// <returns>A list of user view models.</returns>
        public List<UserViewModel> GetUsersForDropdown()
        {
            List<User> usersDb = _userRepository.GetAll();
            return usersDb.Select(x => x.MapToUserViewModel()).ToList();
        }
    }
}