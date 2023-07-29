using BurgerApp.Domain.Models;

namespace BurgerApp.DataAccess.Repositories.Abstraction
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        T GetById(int id);
        int Insert(T entity);
        void Update(T entity);
        void DeleteById(int id);
    }
}