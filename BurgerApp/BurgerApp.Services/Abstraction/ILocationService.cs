using BurgerApp.ViewModels.LocationViewModels;

namespace BurgerApp.Services.Abstraction
{
    public interface ILocationService
    {
        List<LocationListViewModel> GetAllLocations();
        LocationViewModel GetLocationDetails(int id);
        void AddLocation(LocationViewModel locationViewModel);
        LocationViewModel GetLocationForEditing(int id);
        void EditLocation(LocationViewModel locationViewModel);
        void DeleteLocation(int id);
        List<LocationViewModel> GetLocationsForDropdown();
    }
}