using BurgerApp.DataAccess.Data;
using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.Domain.Models;

namespace BurgerApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation
{
    public class LocationRepositoryEntity : ILocationRepository
    {
        private BurgerAppDbContext _burgerAppDbContext;

        public LocationRepositoryEntity(BurgerAppDbContext burgerAppDbContext)
        {
            _burgerAppDbContext = burgerAppDbContext;
        }

        /// <summary>
        /// Deletes a location with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        public void DeleteById(int id)
        {
            Location locationDb = _burgerAppDbContext.Locations.FirstOrDefault(x => x.Id == id);

            if (locationDb == null)
            {
                throw new Exception($"The location with id {id} was not found!");
            }

            _burgerAppDbContext.Locations.Remove(locationDb);
            _burgerAppDbContext.SaveChanges();
        }

        /// <summary>
        /// Gets a list of all locations from the database.
        /// </summary>
        /// <returns>A list of all locations.</returns>
        public List<Location> GetAll()
        {
            return _burgerAppDbContext.Locations.ToList();
        }

        /// <summary>
        /// Gets a location by its address from the database.
        /// </summary>
        /// <param name="address">The address of the location to retrieve.</param>
        /// <returns>The location with the specified address, or null if not found.</returns>
        public Location GetByAddress(string address)
        {
            return _burgerAppDbContext.Locations.FirstOrDefault(l => l.Address == address);
        }

        /// <summary>
        /// Gets a location by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the location to retrieve.</param>
        /// <returns>The location with the specified ID, or null if not found.</returns>
        public Location GetById(int id)
        {
            return _burgerAppDbContext.Locations.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Gets a location by its name from the database.
        /// </summary>
        /// <param name="name">The name of the location to retrieve.</param>
        /// <returns>The location with the specified name, or null if not found.</returns>
        public Location GetByName(string name)
        {
            return _burgerAppDbContext.Locations.FirstOrDefault(l => l.Name == name);
        }

        /// <summary>
        /// Gets an existing location by its name, or creates a new location if not found, in the database.
        /// </summary>
        /// <param name="name">The name of the location to retrieve or create.</param>
        /// <param name="address">The address of the location to retrieve or create.</param>
        /// <returns>The existing or newly created location.</returns>
        public Location GetOrCreateLocation(string name, string address)
        {
            Location locationDb = _burgerAppDbContext.Locations.FirstOrDefault(l => l.Name == name);

            if (locationDb == null)
            {
                locationDb = _burgerAppDbContext.Locations.FirstOrDefault(l => l.Address == address);
                if (locationDb == null)
                {
                    locationDb = new Location
                    {
                        Name = name,
                        Address = address
                    };

                    _burgerAppDbContext.Locations.Add(locationDb);
                    _burgerAppDbContext.SaveChanges();
                }
            }

            return locationDb;
        }

        /// <summary>
        /// Inserts a new location into the database.
        /// </summary>
        /// <param name="entity">The location to insert.</param>
        /// <returns>The ID of the newly inserted location.</returns>
        public int Insert(Location entity)
        {
            _burgerAppDbContext.Locations.Add(entity);
            return _burgerAppDbContext.SaveChanges();
        }

        /// <summary>
        /// Updates an existing location in the database.
        /// </summary>
        /// <param name="entity">The location to update.</param>
        public void Update(Location entity)
        {
            _burgerAppDbContext.Locations.Update(entity);
            _burgerAppDbContext.SaveChanges();
        }
    }
}