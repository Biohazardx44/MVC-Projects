using System.ComponentModel.DataAnnotations;

namespace BurgerApp.ViewModels.OrderViewModels
{
    public class BurgerOrderViewModel
    {
        [Display(Name = "Select Burger To Add")]
        public int BurgerId { get; set; }
        public int OrderId { get; set; }
    }
}