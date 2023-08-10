using BurgerApp.Domain.Enums;

namespace BurgerApp.Domain.Models
{
    public class Burger : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public bool HasFries { get; set; }
        public BurgerSize BurgerSize { get; set; }
        public List<BurgerOrder> BurgerOrders { get; set; }
    }
}