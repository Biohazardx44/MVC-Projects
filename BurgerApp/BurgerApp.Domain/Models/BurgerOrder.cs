namespace BurgerApp.Domain.Models
{
    public class BurgerOrder : BaseEntity
    {
        public Burger Burger { get; set; }
        public int BurgerId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}