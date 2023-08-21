using PizzaApp.ViewModels.OrderViewModels;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.ViewModels.UserViewModels
{
    public class UserDetailsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "User Full Name")]
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<OrderViewModel> Orders { get; set; }
    }
}