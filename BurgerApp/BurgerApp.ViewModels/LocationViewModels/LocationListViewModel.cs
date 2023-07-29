namespace BurgerApp.ViewModels.LocationViewModels
{
    public class LocationListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public TimeSpan OpensAt { get; set; }
        public TimeSpan ClosesAt { get; set; }
    }
}