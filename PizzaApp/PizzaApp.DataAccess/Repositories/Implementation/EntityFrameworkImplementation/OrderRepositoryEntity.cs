using Microsoft.EntityFrameworkCore;
using PizzaApp.DataAccess.Data;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Models;

namespace PizzaApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation
{
    public class OrderRepositoryEntity : IRepository<Order>
    {
        private PizzaAppDbContext _pizzaAppDbContext;

        public OrderRepositoryEntity(PizzaAppDbContext pizzaAppDbContext)
        {
            _pizzaAppDbContext = pizzaAppDbContext;
        }

        /// <summary>
        /// Deletes an order with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        public void DeleteById(int id)
        {
            Order orderDb = _pizzaAppDbContext.Orders.FirstOrDefault(order => order.Id == id);

            if (orderDb == null)
            {
                throw new Exception($"The order with id {id} was not found!");
            }

            _pizzaAppDbContext.Orders.Remove(orderDb);
            _pizzaAppDbContext.SaveChanges();

        }

        /// <summary>
        /// Gets a list of all orders from the database, including associated burger orders and locations.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        public List<Order> GetAll()
        {
            return _pizzaAppDbContext
                .Orders
                .Include(x => x.PizzaOrders)
                .ThenInclude(x => x.Pizza)
                .Include(x => x.User)
                .ToList();
        }

        /// <summary>
        /// Gets an order by its ID from the database, including associated burger orders and location.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The order with the specified ID, or null if not found.</returns>
        public Order GetById(int id)
        {
            return _pizzaAppDbContext
                    .Orders
                   .Include(x => x.PizzaOrders)
                   .ThenInclude(x => x.Pizza)
                   .Include(x => x.User)
                   .FirstOrDefault(x => x.Id == id)!;
        }

        /// <summary>
        /// Inserts a new order into the database.
        /// </summary>
        /// <param name="entity">The order to insert.</param>
        /// <returns>The ID of the newly inserted order.</returns>
        public int Insert(Order entity)
        {
            _pizzaAppDbContext.Orders.Add(entity);
            return _pizzaAppDbContext.SaveChanges();
        }

        /// <summary>
        /// Updates an existing order in the database.
        /// </summary>
        /// <param name="entity">The order to update.</param>
        public void Update(Order entity)
        {
            _pizzaAppDbContext.Orders.Update(entity);
            _pizzaAppDbContext.SaveChanges();
        }
    }
}