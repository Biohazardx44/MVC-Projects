using PizzaApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.ViewModels.OrderViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Delivered")]
        public bool IsDelivered { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Order Location must be at least 5 characters long.")]
        [Display(Name = "Order Location")]
        public string Location { get; set; } = string.Empty;
        [Display(Name = "Pizzas")]
        public List<string>? PizzaNames { get; set; }
        [Display(Name = "User")]
        public int UserId { get; set; }
    }
}