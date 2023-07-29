using BurgerApp.DataAccess.Data;
using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.Domain.Models;

namespace BurgerApp.DataAccess.Repositories.Implementation.StaticDbImplementation
{
    public class LocationRepository : ILocationRepository
    {
        public void DeleteById(int id)
        {
            Location location = StaticDb.Locations.FirstOrDefault(x => x.Id == id);
            if (location == null)
            {
                throw new Exception($"Location with id {id} was not found!");
            }
            StaticDb.Locations.Remove(location);
        }

        public List<Location> GetAll()
        {
            return StaticDb.Locations;
        }

        public Location GetByAddress(string address)
        {
            return StaticDb.Locations.FirstOrDefault(l => l.Address == address);
        }

        public Location GetById(int id)
        {
            return StaticDb.Locations.FirstOrDefault(x => x.Id == id);
        }

        public Location GetByName(string name)
        {
            return StaticDb.Locations.FirstOrDefault(l => l.Name == name);
        }

        public Location GetOrCreateLocation(string name, string address)
        {
            Location locationDb = GetByName(name);
            if (locationDb == null)
            {
                locationDb = GetByAddress(address);
                if (locationDb == null)
                {
                    locationDb = new Location
                    {
                        Name = name,
                        Address = address
                    };

                    int newLocationId = Insert(locationDb);
                    if (newLocationId <= 0)
                    {
                        throw new Exception("An error occurred while saving the new location to the database!");
                    }
                }
            }
            return locationDb;
        }

        public int Insert(Location entity)
        {
            entity.Id = ++StaticDb.LocationId;
            StaticDb.Locations.Add(entity);
            return entity.Id;
        }

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