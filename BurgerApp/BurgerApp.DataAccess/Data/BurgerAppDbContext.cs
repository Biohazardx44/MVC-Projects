using BurgerApp.Domain.Enums;
using BurgerApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerApp.DataAccess.Data
{
    /// <summary>
    /// Represents the database context for the BurgerApp application, responsible for interacting with the underlying database.
    /// </summary>
    public class BurgerAppDbContext : DbContext
    {
        public BurgerAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Burger> Burgers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entity relationships
            modelBuilder.Entity<Order>()
                .HasMany(x => x.BurgerOrders)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Location)
                .WithMany()
                .HasForeignKey(x => x.LocationId);

            modelBuilder.Entity<Burger>()
                .HasMany(x => x.BurgerOrders)
                .WithOne(x => x.Burger)
                .HasForeignKey(x => x.BurgerId);

            // Seed initial data
            modelBuilder.Entity<Burger>()
                .HasData(
                    new Burger
                    {
                        Id = 1,
                        Name = "Chicken Burger",
                        BurgerSize = BurgerSize.Medium,
                        Price = 20,
                        IsVegetarian = false,
                        IsVegan = false,
                        HasFries = true
                    },
                    new Burger
                    {
                        Id = 2,
                        Name = "Veggie Burger",
                        BurgerSize = BurgerSize.Small,
                        Price = 10,
                        IsVegetarian = true,
                        IsVegan = true,
                        HasFries = false
                    },
                    new Burger
                    {
                        Id = 3,
                        Name = "Cheeseburger",
                        BurgerSize = BurgerSize.Large,
                        Price = 35,
                        IsVegetarian = false,
                        IsVegan = false,
                        HasFries = true
                    }
                );

            modelBuilder.Entity<Location>()
                .HasData(
                    new Location
                    {
                        Id = 1,
                        Name = "Quuen St.Burger Shop",
                        Address = "1371 Queen St W",
                        OpensAt = new TimeSpan(8, 0, 0),
                        ClosesAt = new TimeSpan(22, 0, 0),
                    },
                    new Location
                    {
                        Id = 2,
                        Name = "Main St.Burger Shop",
                        Address = "360 Main St",
                        OpensAt = new TimeSpan(9, 0, 0),
                        ClosesAt = new TimeSpan(23, 0, 0),
                    }
                );

            modelBuilder.Entity<Order>()
                .HasData(
                    new Order
                    {
                        Id = 1,
                        Address = "68 King William St",
                        FullName = "Kennedi Williamson",
                        IsDelivered = false,
                        LocationId = 1
                    },
                    new Order
                    {
                        Id = 2,
                        Address = "3550 N Woodlawn St",
                        FullName = "Kendra Heathcote",
                        IsDelivered = true,
                        LocationId = 2
                    }
                );

            modelBuilder.Entity<BurgerOrder>()
                .HasData(
                    new BurgerOrder
                    {
                        Id = 1,
                        BurgerId = 1,
                        OrderId = 1
                    },
                    new BurgerOrder
                    {
                        Id = 2,
                        BurgerId = 3,
                        OrderId = 1
                    },
                    new BurgerOrder
                    {
                        Id = 3,
                        BurgerId = 2,
                        OrderId = 2
                    }
                );
        }
    }
}