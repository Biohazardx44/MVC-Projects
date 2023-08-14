using PizzaApp.Domain.Enums;

namespace PizzaApp.Domain.Models
{
    public class Order : BaseEntity
    {
        public string Location { get; set; } = string.Empty;
        public bool IsDelivered { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<PizzaOrder> PizzaOrders { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}