using BurgerApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BurgerApp.ViewModels.BurgerViewModels
{
    public class BurgerViewModel
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Burger Name must be at least 5 characters long.")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9 .,!@#$%^&*()_+=\-\\[\]{}|;:'""<>,.?/~`]{4,}$", ErrorMessage = "Burger Name must start with a capital letter.")]
        public string Name { get; set; } = string.Empty;
        [Range(5, int.MaxValue, ErrorMessage = "Minimum Price is 5 $.")]
        public int Price { get; set; }
        [Display(Name = "Vegetarian")]
        public bool IsVegetarian { get; set; }
        [Display(Name = "Vegan")]
        public bool IsVegan { get; set; }
        [Display(Name = "Fries")]
        public bool HasFries { get; set; }
        [Display(Name = "Burger Size")]
        public BurgerSize BurgerSize { get; set; }
    }
}