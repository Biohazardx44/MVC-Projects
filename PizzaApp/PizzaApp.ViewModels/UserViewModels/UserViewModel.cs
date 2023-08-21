using System.ComponentModel.DataAnnotations;

namespace PizzaApp.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name must be at least 3 characters long.")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9 .,!@#$%^&*()_+=\-\\[\]{}|;:'""<>,.?/~`]{2,}$", ErrorMessage = "First Name must start with a capital letter.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name must be at least 3 characters long.")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9 .,!@#$%^&*()_+=\-\\[\]{}|;:'""<>,.?/~`]{2,}$", ErrorMessage = "Last Name must start with a capital letter.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Address must be at least 5 characters long.")]
        public string Address { get; set; } = string.Empty;
    }
}