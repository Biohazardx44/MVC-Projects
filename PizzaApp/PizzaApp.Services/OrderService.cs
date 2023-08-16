using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Models;
using PizzaApp.Mappers.Extensions;
using PizzaApp.Services.Abstraction;
using PizzaApp.ViewModels.OrderViewModels;

namespace PizzaApp.Services
{
    public class OrderService : IOrderService
    {
        private IRepository<Order> _orderRepository;
        private IRepository<User> _userRepository;
        private IPizzaRepository _pizzaRepository;

        public OrderService(IRepository<Order> orderRepository,
                            IRepository<User> userRepository,
                            IPizzaRepository pizzaRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _pizzaRepository = pizzaRepository;
        }

        /// <summary>
        /// Adds a pizza to an existing order.
        /// </summary>
        /// <param name="pizzaOrderViewModel">The view model containing the pizza order details.</param>
        public void AddPizzaToEdit(PizzaOrderViewModel pizzaOrderViewModel)
        {
            var pizzaDb = _pizzaRepository.GetById(pizzaOrderViewModel.PizzaId);
            if (pizzaDb == null)
            {
                throw new Exception($"Pizza with id {pizzaOrderViewModel.PizzaId} was not found");
            }

            var orderDb = _orderRepository.GetById(pizzaOrderViewModel.OrderId);
            if (orderDb == null)
            {
                throw new Exception($"Order with id {pizzaOrderViewModel.OrderId} was not found");
            }

            orderDb.PizzaOrders ??= new List<PizzaOrder>();

            var pizzaOrder = pizzaOrderViewModel.MapToPizzaOrder(pizzaDb, orderDb);
            orderDb.PizzaOrders.Add(pizzaOrder);

            _orderRepository.Update(orderDb);
        }

        /// <summary>
        /// Creates a new order in the database.
        /// </summary>
        /// <param name="orderViewModel">The view model containing the order details.</param>
        public void CreateOrder(OrderViewModel orderViewModel)
        {
            User userDb = _userRepository.GetById(orderViewModel.UserId);

            if (userDb == null)
            {
                throw new Exception($"User with id {orderViewModel.UserId} was not found!");
            }

            Order order = new Order();
            orderViewModel.MapToOrder(order, userDb);
            order.User = userDb;

            int newOrderId = _orderRepository.Insert(order);
            if (newOrderId <= 0)
            {
                throw new Exception("An error occurred while saving to the database!");
            }
        }

        /// <summary>
        /// Deletes a pizza from an existing order in the database.
        /// </summary>
        /// <param name="pizzaOrderViewModel">The view model containing the pizza order details.</param>
        public void DeletePizzaFromEdit(PizzaOrderViewModel pizzaOrderViewModel)
        {
            var orderDb = _orderRepository.GetById(pizzaOrderViewModel.OrderId);
            if (orderDb == null)
            {
                throw new Exception($"Order with id {pizzaOrderViewModel.OrderId} was not found!");
            }

            var pizzaOrderToRemove = orderDb.PizzaOrders.FirstOrDefault(x => x.PizzaId == pizzaOrderViewModel.PizzaId);
            if (pizzaOrderToRemove == null)
            {
                throw new Exception($"Pizza with id {pizzaOrderViewModel.PizzaId} was not found in the order"!);
            }

            orderDb.PizzaOrders.Remove(pizzaOrderToRemove);

            _orderRepository.Update(orderDb);
        }

        /// <summary>
        /// Deletes an order from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the order to be deleted.</param>
        public void DeleteOrder(int id)
        {
            Order orderDb = _orderRepository.GetById(id);
            if (orderDb == null)
            {
                throw new Exception($"The order with id {id} was not found!");
            }
            _orderRepository.DeleteById(id);
        }

        /// <summary>
        /// Updates an existing order in the database with the new details.
        /// </summary>
        /// <param name="orderViewModel">The view model containing the updated order details.</param>
        public void EditOrder(OrderViewModel orderViewModel)
        {
            Order orderDb = _orderRepository.GetById(orderViewModel.Id);
            if (orderDb == null)
            {
                throw new Exception($"The order with id {orderViewModel.Id} was not found!");
            }

            User userDb = _userRepository.GetById(orderViewModel.UserId);
            if (userDb == null)
            {
                throw new Exception($"The user with id {orderViewModel.UserId} was not found!");
            }

            orderViewModel.MapToOrder(orderDb, userDb);
            _orderRepository.Update(orderDb);
        }

        /// <summary>
        /// Retrieves a list of all orders from the database.
        /// </summary>
        /// <returns>A list of order view models.</returns>
        public List<OrderListViewModel> GetAllOrders()
        {
            List<Order> dbOrders = _orderRepository.GetAll();
            return dbOrders.Select(order => order.MapToOrderListViewModel()).ToList();
        }

        /// <summary>
        /// Retrieves the details of a specific order from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The view model representing the details of the specified order.</returns>
        public OrderDetailsViewModel GetOrderDetails(int id)
        {
            Order orderDb = _orderRepository.GetById(id);
            if (orderDb == null)
            {
                throw new Exception($"The order with id {id} was not found!");
            }
            return orderDb.MapToOrderDetailsViewModel();
        }

        /// <summary>
        /// Retrieves an order view model for editing based on its ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve for editing.</param>
        /// <returns>The view model representing the order details for editing.</returns>
        public OrderViewModel GetOrderForEditing(int id)
        {
            Order orderDb = _orderRepository.GetById(id);
            if (orderDb == null)
            {
                throw new Exception($"The order with id {id} was not found!");
            }

            return orderDb.MapToOrderViewModel();
        }
    }
}