using PizzaApp.ViewModels.OrderViewModels;

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
        void AddPizzaToOrder(PizzaOrderViewModel pizzaOrderViewModel);
    }
}