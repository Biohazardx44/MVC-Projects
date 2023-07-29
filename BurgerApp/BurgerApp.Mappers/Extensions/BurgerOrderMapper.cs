using BurgerApp.Domain.Models;
using BurgerApp.ViewModels.OrderViewModels;

namespace BurgerApp.Mappers.Extensions
{
    public static class BurgerOrderMapper
    {
        public static BurgerOrder MapToBurgerOrder(this BurgerOrderViewModel burgerOrderViewModel, Burger burgerDb, Order orderDb)
        {
            return new BurgerOrder
            {
                Burger = burgerDb,
                BurgerId = burgerDb.Id,
                Order = orderDb,
                OrderId = orderDb.Id,
            };
        }
    }
}