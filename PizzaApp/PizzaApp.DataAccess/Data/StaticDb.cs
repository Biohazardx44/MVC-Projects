using PizzaApp.Domain.Enums;
using PizzaApp.Domain.Models;

namespace PizzaApp.DataAccess.Data
{
    /// <summary>
    /// A static class representing a simple in-memory database for the PizzaApp application.
    /// </summary>
    public static class StaticDb
    {
        public static int PizzaId { get; set; }
        public static int OrderId { get; set; }
        public static int UserId { get; set; }
        public static List<Pizza> Pizzas { get; set; }
        public static List<Order> Orders { get; set; }
        public static List<User> Users { get; set; }

        // Static constructor to initialize the database with sample data
        static StaticDb()
        {
            PizzaId = 3;
            OrderId = 2;
            UserId = 2;

            Pizzas = new List<Pizza>
            {
                new Pizza
                {
                    Id = 1,
                    Name ="Capricciosa",
                    IsOnPromotion = true,
                    PizzaOrders = new List<PizzaOrder> {}
                },
                new Pizza
                {
                    Id = 2,
                    Name = "Pepperoni",
                    IsOnPromotion = false,
                    PizzaOrders = new List<PizzaOrder> {}
                },
                new Pizza
                {
                    Id = 3,
                    Name="Margherita",
                    IsOnPromotion = false,
                    PizzaOrders = new List<PizzaOrder> {}
                },
            };

            Users = new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Arianna",
                    LastName = "Funk",
                    Address = "796 Main St",
                    Orders = new List<Order> {}
                },
                new User
                {
                    Id = 2,
                    FirstName = "Juana",
                    LastName = "Schmeler",
                    Address = "2600 S Rd",
                    Orders = new List<Order> {}
                }
            };

            Orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    PaymentMethod = PaymentMethod.Card,
                    IsDelivered = true,
                    Location = "Krispy Store",
                    PizzaOrders = new List<PizzaOrder>
                    {
                        new PizzaOrder
                        {   Id = 1,
                            Pizza = Pizzas[0],
                            PizzaId = Pizzas[0].Id,
                            PizzaSize = PizzaSize.Small,
                            OrderId = 1
                        },
                        new PizzaOrder
                        {
                            Id = 2,
                            Pizza = Pizzas[1],
                            PizzaId = Pizzas[1].Id,
                            PizzaSize = PizzaSize.Family,
                            OrderId = 1
                        }
                    },
                    User = Users[0]
                },
                new Order
                {
                    Id = 2,
                    PaymentMethod = PaymentMethod.Cash,
                    IsDelivered = false,
                    Location = "Glamful Store",
                    PizzaOrders = new List<PizzaOrder>
                    {
                        new PizzaOrder
                        {
                            Id = 3,
                            Pizza = Pizzas[1],
                            PizzaId = Pizzas[1].Id,
                            PizzaSize = PizzaSize.Medium,
                            OrderId  = 2
                        }
                    },
                    User = Users [1]
                }
            };
        }
    }
}