using BurgerApp.Domain.Models;

namespace BurgerApp.DataAccess.Repositories.Abstraction
{
    public interface ILocationRepository : IRepository<Location>
    {
        Location GetByName(string name);
        Location GetByAddress(string address);
        Location GetOrCreateLocation(string name, string address);
    }
}