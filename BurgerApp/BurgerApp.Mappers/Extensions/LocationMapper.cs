using BurgerApp.Domain.Models;
using BurgerApp.ViewModels.LocationViewModels;

namespace BurgerApp.Mappers.Extensions
{
    public static class LocationMapper
    {
        public static void MapToLocation(this LocationViewModel locationViewModel, Location location)
        {
            location.Name = locationViewModel.Name;
            location.Address = locationViewModel.Address;
            location.OpensAt = locationViewModel.OpensAt;
            location.ClosesAt = locationViewModel.ClosesAt;
        }

        public static LocationViewModel MapToLocationViewModel(this Location location)
        {
            return new LocationViewModel
            {
                Name = location.Name,
                Address = location.Address,
                OpensAt = location.OpensAt,
                ClosesAt = location.ClosesAt,
            };
        }

        public static LocationListViewModel MapToLocationListViewModel(this Location location)
        {
            return new LocationListViewModel
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                OpensAt = location.OpensAt,
                ClosesAt = location.ClosesAt,
            };
        }
    }
}