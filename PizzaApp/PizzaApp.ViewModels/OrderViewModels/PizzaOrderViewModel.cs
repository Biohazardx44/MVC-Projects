using System.ComponentModel.DataAnnotations;

namespace PizzaApp.ViewModels.OrderViewModels
{
    public class PizzaOrderViewModel
    {
        [Display(Name = "Select Pizza To Add")]
        public int PizzaId { get; set; }
        public int OrderId { get; set; }
    }
}