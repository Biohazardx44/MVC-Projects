using BurgerApp.ViewModels.BurgerViewModels;
using BurgerApp.ViewModels.OrderViewModels;

namespace BurgerApp.Services.Abstraction
{
    public interface IOrderService
    {
        List<OrderListViewModel> GetAllOrders();
        OrderViewModel GetOrderDetails(int id);
        void CreateOrder(OrderViewModel orderViewModel);
        OrderViewModel GetOrderForEditing(int id);
        void EditOrder(OrderViewModel orderViewModel);
        void DeleteOrder(int id);
        void AddBurgerToEdit(BurgerOrderViewModel burgerOrderViewModel);
        void DeleteBurgerFromEdit(BurgerOrderViewModel burgerOrderViewModel);
        List<BurgerViewModel> GetAllBurgers();
        int AverageOrderPrice();
    }
}