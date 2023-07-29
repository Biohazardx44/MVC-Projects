using BurgerApp.DataAccess.Data;
using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.Domain.Models;

namespace BurgerApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation
{
    public class BurgerRepositoryEntity : IRepository<Burger>
    {
        private BurgerAppDbContext _burgerAppDbContext;

        public BurgerRepositoryEntity(BurgerAppDbContext burgerAppDbContext)
        {
            _burgerAppDbContext = burgerAppDbContext;
        }

        public void DeleteById(int id)
        {
            Burger burgerDb = _burgerAppDbContext.Burgers.FirstOrDefault(x => x.Id == id);

            if (burgerDb == null)
            {
                throw new Exception($"The burger with id {id} was not found!");
            }

            _burgerAppDbContext.Burgers.Remove(burgerDb);
            _burgerAppDbContext.SaveChanges();
        }

        public List<Burger> GetAll()
        {
            return _burgerAppDbContext.Burgers.ToList();
        }

        public Burger GetById(int id)
        {
            return _burgerAppDbContext.Burgers.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Burger entity)
        {
            _burgerAppDbContext.Burgers.Add(entity);
            return _burgerAppDbContext.SaveChanges();
        }

        public void Update(Burger entity)
        {
            _burgerAppDbContext.Burgers.Update(entity);
            _burgerAppDbContext.SaveChanges();
        }
    }
}