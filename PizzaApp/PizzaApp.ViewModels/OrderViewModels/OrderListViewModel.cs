namespace PizzaApp.ViewModels.OrderViewModels
{
    public class OrderListViewModel
    {
        public int Id { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public bool IsDelivered { get; set; }
        public List<string> PizzaNames { get; set; }
    }
}