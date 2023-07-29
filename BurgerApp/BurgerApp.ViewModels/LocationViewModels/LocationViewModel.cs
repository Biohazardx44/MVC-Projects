using System.ComponentModel.DataAnnotations;

namespace BurgerApp.ViewModels.LocationViewModels
{
    public class LocationViewModel
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The New Shop Name must be at least 5 characters long.")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9 .,!@#$%^&*()_+=\-\\[\]{}|;:'""<>,.?/~`]{4,}$", ErrorMessage = "The New Shop Name must start with a capital letter.")]
        [Display(Name = "New Shop Name")]
        public string Name { get; set; } = string.Empty;
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The New Shop Address must be at least 5 characters long.")]
        [Display(Name = "New Shop Address")]
        public string Address { get; set; } = string.Empty;
        [Display(Name = "Opens At")]
        public TimeSpan OpensAt { get; set; }
        [Display(Name = "Closes At")]
        public TimeSpan ClosesAt { get; set; }
    }
}