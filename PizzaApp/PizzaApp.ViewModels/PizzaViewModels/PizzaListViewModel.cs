using PizzaApp.Domain.Enums;

namespace PizzaApp.ViewModels.PizzaViewModels
{
    public class PizzaListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public PizzaSize PizzaSize { get; set; }
    }
}