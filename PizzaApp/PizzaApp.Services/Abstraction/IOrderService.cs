﻿using PizzaApp.ViewModels.OrderViewModels;

namespace PizzaApp.Services.Abstraction
{
    public interface IOrderService
    {
        List<OrderListViewModel> GetAllOrders();
        OrderDetailsViewModel GetOrderDetails(int id);
        void CreateOrder(OrderViewModel orderViewModel);
        OrderViewModel GetOrderForEditing(int id);
        void EditOrder(OrderViewModel orderViewModel);
        void DeleteOrder(int id);
        void AddPizzaToEdit(PizzaOrderViewModel pizzaOrderViewModel);
        void DeletePizzaFromEdit(PizzaOrderViewModel pizzaOrderViewModel);
        List<OrderViewModel> GetOrdersForUser(int userId);
    }
}