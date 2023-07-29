namespace BurgerApp.ViewModels.OrderViewModels
{
    public class OrderListViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public string LocationAddress { get; set; } = string.Empty;
        public bool IsDelivered { get; set; }
        public List<string> BurgerNames { get; set; }
    }
}