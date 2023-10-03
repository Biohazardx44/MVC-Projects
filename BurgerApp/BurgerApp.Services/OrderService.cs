using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.Domain.Models;
using BurgerApp.Mappers.Extensions;
using BurgerApp.Services.Abstraction;
using BurgerApp.ViewModels.OrderViewModels;

namespace BurgerApp.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IBurgerRepository _burgerRepository;
        private ILocationRepository _locationRepository;

        public OrderService(IOrderRepository orderRepository,
                            IBurgerRepository burgerRepository,
                            ILocationRepository locationRepository)
        {
            _orderRepository = orderRepository;
            _burgerRepository = burgerRepository;
            _locationRepository = locationRepository;

        }

        /// <summary>
        /// Adds a burger to an existing order.
        /// </summary>
        /// <param name="burgerOrderViewModel">The view model containing the burger order details.</param>
        public void AddBurgerToEdit(BurgerOrderViewModel burgerOrderViewModel)
        {
            var burgerDb = _burgerRepository.GetById(burgerOrderViewModel.BurgerId);
            if (burgerDb == null)
            {
                throw new Exception($"Burger with id {burgerOrderViewModel.BurgerId} was not found");
            }

            var orderDb = _orderRepository.GetById(burgerOrderViewModel.OrderId);
            if (orderDb == null)
            {
                throw new Exception($"Order with id {burgerOrderViewModel.OrderId} was not found");
            }

            orderDb.BurgerOrders ??= new List<BurgerOrder>();

            var burgerOrder = burgerOrderViewModel.MapToBurgerOrder(burgerDb, orderDb);
            orderDb.BurgerOrders.Add(burgerOrder);

            _orderRepository.Update(orderDb);
        }

        /// <summary>
        /// Calculates the average price of all orders in the database.
        /// </summary>
        /// <returns>The average order price.</returns>
        public decimal AverageOrderPrice()
        {
            List<Order> orderDb = _orderRepository.GetAll();
            if (orderDb.Count == 0)
            {
                return 0;
            }

            decimal totalOrderPrice = orderDb.Sum(x => x.BurgerOrders.Sum(x => x.Burger.Price));
            decimal averagePrice = totalOrderPrice / orderDb.Count;

            return averagePrice;
        }

        /// <summary>
        /// Creates a new order in the database.
        /// </summary>
        /// <param name="orderViewModel">The view model containing the order details.</param>
        public void CreateOrder(OrderViewModel orderViewModel)
        {
            Location locationDb = _locationRepository.GetOrCreateLocation(orderViewModel.LocationName, orderViewModel.LocationAddress);

            Order order = new Order();
            orderViewModel.MapToOrder(order, locationDb);
            order.Location = locationDb;

            int newOrderId = _orderRepository.Insert(order);
            if (newOrderId <= 0)
            {
                throw new Exception("An error occurred while saving to the database!");
            }
        }

        /// <summary>
        /// Deletes a burger from an existing order in the database.
        /// </summary>
        /// <param name="burgerOrderViewModel">The view model containing the burger order details.</param>
        public void DeleteBurgerFromEdit(BurgerOrderViewModel burgerOrderViewModel)
        {
            var orderDb = _orderRepository.GetById(burgerOrderViewModel.OrderId);
            if (orderDb == null)
            {
                throw new Exception($"Order with id {burgerOrderViewModel.OrderId} was not found!");
            }

            var burgerOrderToRemove = orderDb.BurgerOrders.FirstOrDefault(burgerOrder => burgerOrder.BurgerId == burgerOrderViewModel.BurgerId);
            if (burgerOrderToRemove == null)
            {
                throw new Exception($"Burger with id {burgerOrderViewModel.BurgerId} was not found in the order"!);
            }

            orderDb.BurgerOrders.Remove(burgerOrderToRemove);

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

            Location locationDb = _locationRepository.GetOrCreateLocation(orderViewModel.LocationName, orderViewModel.LocationAddress);

            orderViewModel.MapToOrder(orderDb, locationDb);
            _orderRepository.Update(orderDb);
        }

        /// <summary>
        /// Retrieves a list of all orders from the database.
        /// </summary>
        /// <returns>A list of view models representing all orders.</returns>
        public List<OrderListViewModel> GetAllOrders()
        {
            List<Order> dbOrders = _orderRepository.GetAll();
            return dbOrders.Select(x => x.MapToOrderListViewModel()).ToList();
        }

        /// <summary>
        /// Retrieves the details of a specific order from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The view model representing the details of the specified order.</returns>
        public OrderViewModel GetOrderDetails(int id)
        {
            Order orderDb = _orderRepository.GetById(id);
            if (orderDb == null)
            {
                throw new Exception($"The order with id {id} was not found!");
            }
            return orderDb.MapToOrderViewModel();
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