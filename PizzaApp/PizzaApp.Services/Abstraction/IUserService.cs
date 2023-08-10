using PizzaApp.ViewModels.UserViewModels;

namespace PizzaApp.Services.Abstraction
{
    public interface IUserService
    {
        List<UserViewModel> GetUsersForDropdown();
    }
}