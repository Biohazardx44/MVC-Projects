﻿using PizzaApp.DataAccess.Data;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Models;

namespace PizzaApp.DataAccess.Repositories.Implementation.StaticDbImplementation
{
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// Deletes an order by its ID from the StaticDb.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <exception cref="Exception">Thrown when the order with the specified ID is not found.</exception>
        public void DeleteById(int id)
        {
            Order order = StaticDb.Orders.FirstOrDefault(order => order.Id == id);
            if (order == null)
            {
                throw new Exception($"Order with id {id} was not found!");
            }
            StaticDb.Orders.Remove(order);
        }

        /// <summary>
        /// Gets all orders from the StaticDb.
        /// </summary>
        /// <returns>A list of all orders in the StaticDb.</returns>
        public List<Order> GetAll()
        {
            return StaticDb.Orders;
        }

        /// <summary>
        /// Gets an order by its ID from the StaticDb.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The order with the specified ID if found; otherwise, null.</returns>
        public Order GetById(int id)
        {
            return StaticDb.Orders.FirstOrDefault(order => order.Id == id);
        }

        /// <summary>
        /// Inserts a new order into the StaticDb.
        /// </summary>
        /// <param name="entity">The order entity to insert.</param>
        /// <returns>The ID of the newly inserted order.</returns>
        public int Insert(Order entity)
        {
            entity.Id = ++StaticDb.OrderId;
            StaticDb.Orders.Add(entity);
            return entity.Id;
        }

        /// <summary>
        /// Updates an existing order in the StaticDb.
        /// </summary>
        /// <param name="entity">The updated order entity.</param>
        /// <exception cref="Exception">Thrown when the order with the specified ID is not found.</exception>
        public void Update(Order entity)
        {
            Order order = StaticDb.Orders.FirstOrDefault(order => order.Id == entity.Id);
            if (order == null)
            {
                throw new Exception($"Order with id {entity.Id} was not found!");
            }
            int index = StaticDb.Orders.IndexOf(order);
            StaticDb.Orders[index] = entity;
        }

        /// <summary>
        /// Retrieves a list of orders associated with a specific user from the in-memory database.
        /// </summary>
        /// <param name="userId">The ID of the user for whom to retrieve orders.</param>
        /// <returns>A list of orders associated with the specified user.</returns>
        public List<Order> GetOrdersForUser(int userId)
        {
            return StaticDb.Orders.Where(order => order.User.Id == userId).ToList();
        }
    }
}