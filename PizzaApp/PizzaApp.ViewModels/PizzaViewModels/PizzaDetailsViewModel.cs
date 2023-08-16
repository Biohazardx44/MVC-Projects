using PizzaApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.ViewModels.PizzaViewModels
{
    public class PizzaDetailsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Pizza Name")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Promotion")]
        public bool IsOnPromotion { get; set; }
        [Display(Name = "Pizza Size")]
        public PizzaSize PizzaSize { get; set; }
    }
}