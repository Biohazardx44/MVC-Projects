using BurgerApp.DataAccess.Data;
using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.Domain.Models;

namespace BurgerApp.DataAccess.Repositories.Implementation.StaticDbImplementation
{
    public class OrderRepository : IRepository<Order>
    {
        public void DeleteById(int id)
        {
            Order order = StaticDb.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                throw new Exception($"Order with id {id} was not found!");
            }
            StaticDb.Orders.Remove(order);
        }

        public List<Order> GetAll()
        {
            return StaticDb.Orders;
        }

        public Order GetById(int id)
        {
            return StaticDb.Orders.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Order entity)
        {
            entity.Id = ++StaticDb.OrderId;
            StaticDb.Orders.Add(entity);
            return entity.Id;
        }

        public void Update(Order entity)
        {
            Order order = StaticDb.Orders.FirstOrDefault(x => x.Id == entity.Id);
            if (order == null)
            {
                throw new Exception($"Order with id {entity.Id} was not found!");
            }
            int index = StaticDb.Orders.IndexOf(order);
            StaticDb.Orders[index] = entity;
        }
    }
}