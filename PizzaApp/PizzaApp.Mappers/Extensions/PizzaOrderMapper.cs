using PizzaApp.Domain.Models;
using PizzaApp.ViewModels.OrderViewModels;

namespace PizzaApp.Mappers.Extensions
{
    public static class PizzaOrderMapper
    {
        public static PizzaOrder MapToPizzaOrder(this PizzaOrderViewModel pizzaOrderViewModel, Pizza pizzaDb, Order orderDb)
        {
            return new PizzaOrder
            {
                Pizza = pizzaDb,
                PizzaId = pizzaDb.Id,
                PizzaSize = pizzaDb.PizzaSize,
                Order = orderDb,
                OrderId = orderDb.Id,
            };
        }
    }
}