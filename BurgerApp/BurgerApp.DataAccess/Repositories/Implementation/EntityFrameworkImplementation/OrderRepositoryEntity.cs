using BurgerApp.DataAccess.Data;
using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation
{
    public class OrderRepositoryEntity : IRepository<Order>
    {
        private BurgerAppDbContext _burgerAppDbContext;

        public OrderRepositoryEntity(BurgerAppDbContext burgerAppDbContext)
        {
            _burgerAppDbContext = burgerAppDbContext;
        }

        /// <summary>
        /// Deletes an order with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        public void DeleteById(int id)
        {
            Order orderDb = _burgerAppDbContext.Orders.FirstOrDefault(order => order.Id == id);

            if (orderDb == null)
            {
                throw new Exception($"The order with id {id} was not found!");
            }

            _burgerAppDbContext.Orders.Remove(orderDb);
            _burgerAppDbContext.SaveChanges();

        }

        /// <summary>
        /// Gets a list of all orders from the database, including associated burger orders and locations.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        public List<Order> GetAll()
        {
            return _burgerAppDbContext
                .Orders
                .Include(x => x.BurgerOrders)
                .ThenInclude(x => x.Burger)
                .Include(x => x.Location)
                .ToList();
        }

        /// <summary>
        /// Gets an order by its ID from the database, including associated burger orders and location.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The order with the specified ID, or null if not found.</returns>
        public Order GetById(int id)
        {
            return _burgerAppDbContext
                    .Orders
                   .Include(x => x.BurgerOrders)
                   .ThenInclude(x => x.Burger)
                   .Include(x => x.Location)
                   .FirstOrDefault(x => x.Id == id)!;
        }

        /// <summary>
        /// Inserts a new order into the database.
        /// </summary>
        /// <param name="entity">The order to insert.</param>
        /// <returns>The ID of the newly inserted order.</returns>
        public int Insert(Order entity)
        {
            _burgerAppDbContext.Orders.Add(entity);
            return _burgerAppDbContext.SaveChanges();
        }

        /// <summary>
        /// Updates an existing order in the database.
        /// </summary>
        /// <param name="entity">The order to update.</param>
        public void Update(Order entity)
        {
            _burgerAppDbContext.Orders.Update(entity);
            _burgerAppDbContext.SaveChanges();
        }
    }
}