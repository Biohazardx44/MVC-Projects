using BurgerApp.Domain.Enums;

namespace BurgerApp.ViewModels.BurgerViewModels
{
    public class BurgerListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public bool HasFries { get; set; }
        public BurgerSize BurgerSize { get; set; }
    }
}