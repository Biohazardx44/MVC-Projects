using BurgerApp.DataAccess.Data;
using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation;
using BurgerApp.DataAccess.Repositories.Implementation.StaticDbImplementation;
using BurgerApp.Domain.Models;
using BurgerApp.Services;
using BurgerApp.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IBurgerService, BurgerService>();
            services.AddTransient<ILocationService, LocationService>();
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            // StaticDb
            //services.AddTransient<IRepository<Order>, OrderRepository>();
            //services.AddTransient<IRepository<Burger>, BurgerRepository>();
            //services.AddTransient<ILocationRepository, LocationRepository>();

            // Entity Framework
            services.AddTransient<IRepository<Order>, OrderRepositoryEntity>();
            services.AddTransient<IRepository<Burger>, BurgerRepositoryEntity>();
            services.AddTransient<ILocationRepository, LocationRepositoryEntity>();
        }

        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BurgerAppDbContext>(options =>
                options.UseSqlServer(connectionString)
            );
        }
    }
}