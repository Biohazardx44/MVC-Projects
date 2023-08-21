using PizzaApp.Domain.Models;
using PizzaApp.ViewModels.UserViewModels;

namespace PizzaApp.Mappers.Extensions
{
    public static class UserMapper
    {
        public static void MapToUser(this UserViewModel userViewModel, User user)
        {
            user.Id = userViewModel.Id;
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Address = userViewModel.Address;
        }

        public static UserViewModel MapToUserViewModel(this User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = $"{user.FirstName} {user.LastName}",
                Address = user.Address,
            };
        }

        public static UserListViewModel MapToUserListViewModel(this User user)
        {
            return new UserListViewModel
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}"
            };
        }

        public static UserDetailsViewModel MapToUserDetailsViewModel(this User user)
        {
            return new UserDetailsViewModel
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Address = user.Address,
                Orders = user.Orders?.Select(order => order.MapToOrderViewModel()).ToList()
            };
        }
    }
}