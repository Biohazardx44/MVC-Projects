using PizzaApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.ViewModels.OrderViewModels
{
    public class PizzaOrderViewModel
    {
        public int OrderId { get; set; }
        [Display(Name = "Select Pizza To Add")]
        public int PizzaId { get; set; }
        [Display(Name = "Select Pizza Size")]
        public PizzaSize PizzaSize { get; set; }
    }
}