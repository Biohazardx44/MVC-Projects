using PizzaApp.ViewModels.UserViewModels;

namespace PizzaApp.Services.Abstraction
{
    public interface IUserService
    {
        List<UserListViewModel> GetAllUsers();
        UserDetailsViewModel GetUserDetails(int id);
        void AddUser(UserViewModel userViewModel);
        UserViewModel GetUserForEditing(int id);
        void EditUser(UserViewModel userViewModel);
        void DeleteUser(int id);
        List<UserViewModel> GetUsersForDropdown();
    }
}