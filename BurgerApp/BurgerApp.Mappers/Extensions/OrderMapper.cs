using BurgerApp.Domain.Models;
using BurgerApp.ViewModels.OrderViewModels;

namespace BurgerApp.Mappers.Extensions
{
    public static class OrderMapper
    {
        public static void MapToOrder(this OrderViewModel orderViewModel, Order order, Location location)
        {
            order.FullName = orderViewModel.FullName;
            order.Address = orderViewModel.Address;
            order.IsDelivered = orderViewModel.IsDelivered;
            order.Location = location;
        }

        public static OrderViewModel MapToOrderViewModel(this Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                FullName = order.FullName,
                Address = order.Address,
                LocationName = order.Location?.Name,
                LocationAddress = order.Location?.Address,
                IsDelivered = order.IsDelivered,
                BurgerNames = order.BurgerOrders?.Select(po => po.Burger.Name).ToList(),
                LocationId = order.LocationId
            };
        }

        public static OrderListViewModel MapToOrderListViewModel(this Order order)
        {
            return new OrderListViewModel
            {
                Id = order.Id,
                FullName = order.FullName,
                Address = order.Address,
                LocationName = order.Location?.Name,
                LocationAddress = order.Location?.Address,
                IsDelivered = order.IsDelivered,
                BurgerNames = order.BurgerOrders?.Select(po => po.Burger.Name).ToList()
            };
        }
    }
}