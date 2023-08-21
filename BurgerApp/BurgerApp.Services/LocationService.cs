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

        /// <summary>
        /// Adds a new location to the database.
        /// </summary>
        /// <param name="locationViewModel">The view model containing the location details.</param>
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

        /// <summary>
        /// Deletes a location from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the location to be deleted.</param>
        public void DeleteLocation(int id)
        {
            Location locationDb = _locationRepository.GetById(id);
            if (locationDb == null)
            {
                throw new Exception($"The location with id {id} was not found!");
            }
            _locationRepository.DeleteById(id);
        }

        /// <summary>
        /// Updates an existing location in the database with the new details.
        /// </summary>
        /// <param name="locationViewModel">The view model containing the updated location details.</param>
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

        /// <summary>
        /// Retrieves a list of all locations from the database.
        /// </summary>
        /// <returns>A list of view models representing all locations.</returns>
        public List<LocationListViewModel> GetAllLocations()
        {
            List<Location> locationsDb = _locationRepository.GetAll();
            return locationsDb.Select(x => x.MapToLocationListViewModel()).ToList();
        }

        /// <summary>
        /// Retrieves the details of a specific location from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the location to retrieve.</param>
        /// <returns>The view model representing the details of the specified location.</returns>
        public LocationViewModel GetLocationDetails(int id)
        {
            Location locationDb = _locationRepository.GetById(id);
            if (locationDb == null)
            {
                throw new Exception($"The location with id {id} was not found!");
            }
            return locationDb.MapToLocationViewModel();
        }

        /// <summary>
        /// Retrieves a location view model for editing based on its ID.
        /// </summary>
        /// <param name="id">The ID of the location to retrieve for editing.</param>
        /// <returns>The view model representing the location details for editing.</returns>
        public LocationViewModel GetLocationForEditing(int id)
        {
            Location locationDb = _locationRepository.GetById(id);
            if (locationDb == null)
            {
                throw new Exception($"The location with id {id} was not found!");
            }

            return locationDb.MapToLocationViewModel();
        }

        /// <summary>
        /// Retrieves a list of location view models for use in dropdowns.
        /// </summary>
        /// <returns>A list of location view models.</returns>
        public List<LocationViewModel> GetLocationsForDropdown()
        {
            List<Location> locationsDb = _locationRepository.GetAll();
            return locationsDb.Select(x => x.MapToLocationViewModel()).ToList();
        }
    }
}