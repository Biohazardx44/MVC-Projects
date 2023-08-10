using PizzaApp.Domain.Models;
using PizzaApp.ViewModels.UserViewModels;

namespace PizzaApp.Mappers.Extensions
{
    public static class UserMapper
    {
        public static UserViewModel MapToUserViewModel(this User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}"
            };
        }
    }
}