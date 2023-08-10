using BurgerApp.DataAccess.Data;
using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.Domain.Models;

namespace BurgerApp.DataAccess.Repositories.Implementation.StaticDbImplementation
{
    public class LocationRepository : ILocationRepository
    {
        /// <summary>
        /// Deletes a location by its ID from the StaticDb.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        /// <exception cref="Exception">Thrown when the location with the specified ID is not found.</exception>
        public void DeleteById(int id)
        {
            Location location = StaticDb.Locations.FirstOrDefault(x => x.Id == id);
            if (location == null)
            {
                throw new Exception($"Location with id {id} was not found!");
            }
            StaticDb.Locations.Remove(location);
        }

        /// <summary>
        /// Gets all locations from the StaticDb.
        /// </summary>
        /// <returns>A list of all locations in the StaticDb.</returns>
        public List<Location> GetAll()
        {
            return StaticDb.Locations;
        }

        /// <summary>
        /// Gets a location by its address from the StaticDb.
        /// </summary>
        /// <param name="address">The address of the location to retrieve.</param>
        /// <returns>The location with the specified address if found; otherwise, null.</returns>
        public Location GetByAddress(string address)
        {
            return StaticDb.Locations.FirstOrDefault(l => l.Address == address);
        }

        /// <summary>
        /// Gets a location by its ID from the StaticDb.
        /// </summary>
        /// <param name="id">The ID of the location to retrieve.</param>
        /// <returns>The location with the specified ID if found; otherwise, null.</returns>
        public Location GetById(int id)
        {
            return StaticDb.Locations.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Gets a location by its name from the StaticDb.
        /// </summary>
        /// <param name="name">The name of the location to retrieve.</param>
        /// <returns>The location with the specified name if found; otherwise, null.</returns>
        public Location GetByName(string name)
        {
            return StaticDb.Locations.FirstOrDefault(l => l.Name == name);
        }

        /// <summary>
        /// Gets an existing location from the StaticDb by name or address, or creates a new location if not found.
        /// </summary>
        /// <param name="name">The name of the location to retrieve or create.</param>
        /// <param name="address">The address of the location to retrieve or create.</param>
        /// <returns>The existing location with the specified name or address, or a new location if not found.</returns>
        public Location GetOrCreateLocation(string name, string address)
        {
            Location location = GetByName(name) ?? GetByAddress(address);

            if (location == null)
            {
                location = new Location
                {
                    Name = name,
                    Address = address
                };

                int newLocationId = Insert(location);
                if (newLocationId <= 0)
                {
                    throw new Exception("An error occurred while saving the new location to the database!");
                }
            }

            return location;
        }

        /// <summary>
        /// Inserts a new location into the StaticDb.
        /// </summary>
        /// <param name="entity">The location entity to insert.</param>
        /// <returns>The ID of the newly inserted location.</returns>
        public int Insert(Location entity)
        {
            entity.Id = ++StaticDb.LocationId;
            StaticDb.Locations.Add(entity);
            return entity.Id;
        }

        /// <summary>
        /// Updates an existing location in the StaticDb.
        /// </summary>
        /// <param name="entity">The updated location entity.</param>
        /// <exception cref="Exception">Thrown when the location with the specified ID is not found.</exception>
        public void Update(Location entity)
        {
            Location location = StaticDb.Locations.FirstOrDefault(x => x.Id == entity.Id);
            if (location == null)
            {
                throw new Exception($"Location with id {entity.Id} was not found!");
            }
            int index = StaticDb.Locations.IndexOf(location);
            StaticDb.Locations[index] = entity;
        }
    }
}