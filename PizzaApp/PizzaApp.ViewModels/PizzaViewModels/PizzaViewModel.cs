using PizzaApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.ViewModels.PizzaViewModels
{
    public class PizzaViewModel
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Pizza Name must be at least 5 characters long.")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9 .,!@#$%^&*()_+=\-\\[\]{}|;:'""<>,.?/~`]{4,}$", ErrorMessage = "Pizza Name must start with a capital letter.")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Promotion")]
        public bool IsOnPromotion { get; set; }
        [Display(Name = "Pizza Size")]
        public PizzaSize PizzaSize { get; set; }
    }
}