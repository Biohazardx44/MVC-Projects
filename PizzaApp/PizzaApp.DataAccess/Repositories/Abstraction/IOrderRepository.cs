using PizzaApp.Domain.Models;

namespace PizzaApp.DataAccess.Repositories.Abstraction
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetOrdersForUser(int userId);
    }
}