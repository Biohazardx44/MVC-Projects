using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Models;
using PizzaApp.Mappers.Extensions;
using PizzaApp.Services.Abstraction;
using PizzaApp.ViewModels.UserViewModels;

namespace PizzaApp.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
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