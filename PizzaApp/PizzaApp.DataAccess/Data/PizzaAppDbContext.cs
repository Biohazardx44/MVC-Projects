using Microsoft.EntityFrameworkCore;
using PizzaApp.Domain.Enums;
using PizzaApp.Domain.Models;

namespace PizzaApp.DataAccess.Data
{
    /// <summary>
    /// Represents the database context for the PizzaApp application, responsible for interacting with the underlying database.
    /// </summary>
    public class PizzaAppDbContext : DbContext
    {
        public PizzaAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entity relationships
            modelBuilder.Entity<Order>()
                .HasMany(x => x.PizzaOrders)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<Pizza>()
                .HasMany(x => x.PizzaOrders)
                .WithOne(x => x.Pizza)
                .HasForeignKey(x => x.PizzaId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            // Seed initial data
            modelBuilder.Entity<Pizza>()
                .HasData(
                    new Pizza
                    {
                        Id = 1,
                        Name = "Capricciosa",
                        IsOnPromotion = true
                    },
                    new Pizza
                    {
                        Id = 2,
                        Name = "Pepperoni",
                        IsOnPromotion = false
                    },
                    new Pizza
                    {
                        Id = 3,
                        Name = "Margherita",
                        IsOnPromotion = false
                    }
                );

            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = 1,
                        FirstName = "Arianna",
                        LastName = "Funk",
                        Address = "796 Main St"
                    },
                    new User
                    {
                        Id = 2,
                        FirstName = "Juana",
                        LastName = "Schmeler",
                        Address = "2600 S Rd"
                    }
                );

            modelBuilder.Entity<Order>()
                .HasData(
                    new Order
                    {
                        Id = 1,
                        PaymentMethod = PaymentMethod.Card,
                        IsDelivered = true,
                        Location = "Krispy Store",
                        UserId = 1
                    },
                    new Order
                    {
                        Id = 2,
                        PaymentMethod = PaymentMethod.Cash,
                        IsDelivered = false,
                        Location = "Glamful Store",
                        UserId = 2
                    }
                );

            modelBuilder.Entity<PizzaOrder>()
                .HasData(
                    new PizzaOrder
                    {
                        Id = 1,
                        OrderId = 1,
                        PizzaId = 1,
                        PizzaSize = PizzaSize.Small
                    },
                    new PizzaOrder
                    {
                        Id = 2,
                        OrderId = 1,
                        PizzaId = 2,
                        PizzaSize = PizzaSize.Family
                    },
                    new PizzaOrder
                    {
                        Id = 3,
                        OrderId = 2,
                        PizzaId = 2,
                        PizzaSize = PizzaSize.Medium
                    }
                );
        }
    }
}