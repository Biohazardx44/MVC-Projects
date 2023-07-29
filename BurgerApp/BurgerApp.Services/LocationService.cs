using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.Domain.Models;
using BurgerApp.Mappers.Extensions;
using BurgerApp.Services.Abstraction;
using BurgerApp.ViewModels.LocationViewModels;

namespace BurgerApp.Services
{
    public class LocationService : ILocationService
    {
        private ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public void AddLocation(LocationViewModel locationViewModel)
        {
            Location location = new Location();

            locationViewModel.MapToLocation(location);

            int newLocationId = _locationRepository.Insert(location);
            if (newLocationId <= 0)
            {
                throw new Exception("An error occurred while saving to the database!");
            }
        }

        public void DeleteLocation(int id)
        {
            Location locationDb = _locationRepository.GetById(id);
            if (locationDb == null)
            {
                throw new Exception($"The location with id {id} was not found!");
            }
            _locationRepository.DeleteById(id);
        }

        public void EditLocation(LocationViewModel locationViewModel)
        {
            Location locationDb = _locationRepository.GetById(locationViewModel.Id);
            if (locationDb == null)
            {
                throw new Exception($"The location with id {locationViewModel.Id} was not found!");
            }

            locationViewModel.MapToLocation(locationDb);

            _locationRepository.Update(locationDb);
        }

        public List<LocationListViewModel> GetAllLocations()
        {
            List<Location> locationsDb = _locationRepository.GetAll();
            return locationsDb.Select(x => x.MapToLocationListViewModel()).ToList();
        }

        public LocationViewModel GetLocationDetails(int id)
        {
            Location locationDb = _locationRepository.GetById(id);
            if (locationDb == null)
            {
                throw new Exception($"The location with id {id} was not found!");
            }
            return locationDb.MapToLocationViewModel();
        }

        public LocationViewModel GetLocationForEditing(int id)
        {
            Location locationDb = _locationRepository.GetById(id);
            if (locationDb == null)
            {
                throw new Exception($"The location with id {id} was not found!");
            }

            return locationDb.MapToLocationViewModel();
        }

        public List<LocationViewModel> GetLocationsForDropdown()
        {
            List<Location> locationsDb = _locationRepository.GetAll();
            return locationsDb.Select(x => x.MapToLocationViewModel()).ToList();
        }
    }
}