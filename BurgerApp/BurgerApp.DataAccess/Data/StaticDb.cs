using BurgerApp.Domain.Enums;
using BurgerApp.Domain.Models;

namespace BurgerApp.DataAccess.Data
{
    public static class StaticDb
    {
        public static int BurgerId { get; set; }
        public static int OrderId { get; set; }
        public static int LocationId { get; set; }
        public static List<Burger> Burgers { get; set; }
        public static List<Order> Orders { get; set; }
        public static List<Location> Locations { get; set; }

        static StaticDb()
        {
            BurgerId = 3;
            OrderId = 2;
            LocationId = 2;

            Burgers = new List<Burger>
            {
                new Burger
                {
                    Id = 1,
                    Name = "Chicken Burger",
                    BurgerSize = BurgerSize.Medium,
                    Price = 20,
                    IsVegetarian = false,
                    IsVegan = false,
                    HasFries = true,
                    BurgerOrders = new List<BurgerOrder> {}
                },
                new Burger
                {
                    Id= 2,
                    Name = "Veggie Burger",
                    BurgerSize = BurgerSize.Small,
                    Price = 10,
                    IsVegetarian = true,
                    IsVegan = true,
                    HasFries = false,
                    BurgerOrders = new List<BurgerOrder> {}
                },
                new Burger
                {
                    Id = 3,
                    Name = "Cheeseburger",
                    BurgerSize = BurgerSize.Large,
                    Price = 35,
                    IsVegetarian = false,
                    IsVegan = false,
                    HasFries = true,
                    BurgerOrders = new List<BurgerOrder> {}
                },
            };

            Locations = new List<Location>
            {
                new Location
                {
                    Id = 1,
                    Name ="Quuen St.Burger Shop",
                    Address ="1371 Queen St W",
                    OpensAt = new TimeSpan(8, 0, 0),
                    ClosesAt = new TimeSpan(22, 0, 0),
                },
                new Location
                {
                    Id = 2,
                    Name ="Main St.Burger Shop",
                    Address ="360 Main St",
                    OpensAt = new TimeSpan(9, 0, 0),
                    ClosesAt = new TimeSpan(23, 0, 0),
                },
            };

            Orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    Address = "68 King William St",
                    FullName = "Kennedi Williamson",
                    IsDelivered = false,
                    BurgerOrders = new List<BurgerOrder>
                    {
                        new BurgerOrder
                        {
                            Id = 1,
                            Burger = Burgers[0],
                            BurgerId = Burgers[0].Id,
                            OrderId = 1
                        },
                        new BurgerOrder
                        {
                            Id = 2,
                            Burger = Burgers[2],
                            BurgerId = Burgers[2].Id,
                            OrderId = 1
                        },
                    },
                    Location = Locations[0]
                },
                new Order
                {
                    Id = 2,
                    Address = "3550 N Woodlawn St",
                    FullName = "Kendra Heathcote",
                    IsDelivered = true,
                    BurgerOrders = new List<BurgerOrder>
                    {
                        new BurgerOrder
                        {
                            Id = 3,
                            Burger = Burgers[1],
                            BurgerId = Burgers[1].Id,
                            OrderId = 2
                        },
                    },
                    Location = Locations[1]
                },
            };
        }
    }
}