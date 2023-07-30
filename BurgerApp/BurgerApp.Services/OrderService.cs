using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.Domain.Models;
using BurgerApp.Mappers.Extensions;
using BurgerApp.Services.Abstraction;
using BurgerApp.ViewModels.BurgerViewModels;
using BurgerApp.ViewModels.OrderViewModels;

namespace BurgerApp.Services
{
    public class OrderService : IOrderService
    {
        private IRepository<Order> _orderRepository;
        private IRepository<Burger> _burgerRepository;
        private ILocationRepository _locationRepository;

        public OrderService(IRepository<Order> orderRepository,
                            IRepository<Burger> burgerRepository,
                            ILocationRepository locationRepository)
        {
            _orderRepository = orderRepository;
            _burgerRepository = burgerRepository;
            _locationRepository = locationRepository;

        }

        public void AddBurger(BurgerOrderViewModel burgerOrderViewModel)
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

        public int AverageOrderPrice()
        {
            List<Order> orderDb = _orderRepository.GetAll();
            if (orderDb.Count == 0)
            {
                return 0;
            }

            int totalOrderPrice = orderDb.Sum(x => x.BurgerOrders.Sum(x => x.Burger.Price));
            int averagePrice = totalOrderPrice / orderDb.Count;

            return averagePrice;
        }

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

        public void DeleteBurger(BurgerOrderViewModel burgerOrderViewModel)
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

        public void DeleteOrder(int id)
        {
            Order orderDb = _orderRepository.GetById(id);
            if (orderDb == null)
            {
                throw new Exception($"The order with id {id} was not found!");
            }
            _orderRepository.DeleteById(id);
        }

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

        public List<BurgerViewModel> GetAllBurgers()
        {
            List<Burger> burgers = _burgerRepository.GetAll();
            return burgers.Select(x => x.MapToBurgerViewModel()).ToList();
        }

        public List<OrderListViewModel> GetAllOrders()
        {
            List<Order> dbOrders = _orderRepository.GetAll();
            return dbOrders.Select(x => x.MapToOrderListViewModel()).ToList();
        }

        public OrderViewModel GetOrderDetails(int id)
        {
            Order orderDb = _orderRepository.GetById(id);
            if (orderDb == null)
            {
                throw new Exception($"The order with id {id} was not found!");
            }
            return orderDb.MapToOrderViewModel();
        }

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