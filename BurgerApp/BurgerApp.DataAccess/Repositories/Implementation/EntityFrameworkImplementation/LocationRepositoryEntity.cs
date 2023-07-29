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

        public List<Location> GetAll()
        {
            return _burgerAppDbContext.Locations.ToList();
        }

        public Location GetByAddress(string address)
        {
            return _burgerAppDbContext.Locations.FirstOrDefault(l => l.Address == address);
        }

        public Location GetById(int id)
        {
            return _burgerAppDbContext.Locations.FirstOrDefault(x => x.Id == id);
        }

        public Location GetByName(string name)
        {
            return _burgerAppDbContext.Locations.FirstOrDefault(l => l.Name == name);
        }

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

        public int Insert(Location entity)
        {
            _burgerAppDbContext.Locations.Add(entity);
            return _burgerAppDbContext.SaveChanges();
        }

        public void Update(Location entity)
        {
            _burgerAppDbContext.Locations.Update(entity);
            _burgerAppDbContext.SaveChanges();
        }
    }
}