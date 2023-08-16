using PizzaApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.ViewModels.OrderViewModels
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Delivered")]
        public bool IsDelivered { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Order Location")]
        public string Location { get; set; } = string.Empty;
        [Display(Name = "User")]
        public string UserFullName { get; set; } = string.Empty;
        [Display(Name = "Pizzas")]
        public List<string> PizzaNames { get; set; }
        public List<PizzaSize> PizzaSizes { get; set; }
    }
}