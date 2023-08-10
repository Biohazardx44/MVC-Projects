namespace BurgerApp.ViewModels.LocationViewModels
{
    public class LocationListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime OpensAt { get; set; }
        public DateTime ClosesAt { get; set; }
    }
}