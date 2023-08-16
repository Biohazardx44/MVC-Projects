using PizzaApp.Domain.Models;
using PizzaApp.ViewModels.OrderViewModels;

namespace PizzaApp.Mappers.Extensions
{
    public static class OrderMapper
    {
        public static void MapToOrder(this OrderViewModel orderViewModel, Order order, User user)
        {
            order.IsDelivered = orderViewModel.IsDelivered;
            order.Location = orderViewModel.Location;
            order.PaymentMethod = orderViewModel.PaymentMethod;
            order.User = user;
        }

        public static OrderViewModel MapToOrderViewModel(this Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                IsDelivered = order.IsDelivered,
                Location = order.Location,
                PaymentMethod = order.PaymentMethod,
                PizzaNames = order.PizzaOrders?.Select(x => x.Pizza.Name).ToList(),
                UserId = order.UserId
            };
        }

        public static OrderListViewModel MapToOrderListViewModel(this Order order)
        {
            return new OrderListViewModel
            {
                Id = order.Id,
                IsDelivered = order.IsDelivered,
                UserFullName = $"{order.User.FirstName} {order.User.LastName}",
                PizzaNames = order.PizzaOrders?.Select(x => x.Pizza.Name).ToList()
            };
        }

        public static OrderDetailsViewModel MapToOrderDetailsViewModel(this Order order)
        {
            return new OrderDetailsViewModel
            {
                IsDelivered = order.IsDelivered,
                PaymentMethod = order.PaymentMethod,
                Location = order.Location,
                UserFullName = $"{order.User.FirstName} {order.User.LastName}",
                PizzaNames = order.PizzaOrders?.Select(x => x.Pizza.Name).ToList(),
                PizzaSizes = order.PizzaOrders?.Select(x => x.Pizza.PizzaSize).ToList(),
                Id = order.Id
            };
        }
    }
}