using BurgerApp.ViewModels.BurgerViewModels;
using BurgerApp.ViewModels.LocationViewModels;

namespace BurgerApp.ViewModels.HomeViewModels
{
    public class HomeIndexViewModel
    {
        public int OrderCount { get; set; }
        public List<BurgerViewModel> MostPopularBurgers { get; set; }
        public int OrderAveragePrice { get; set; }
        public List<LocationListViewModel> BurgerLocations { get; set; }
    }
}