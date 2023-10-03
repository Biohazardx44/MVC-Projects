using BurgerApp.DataAccess.Data;
using BurgerApp.DataAccess.Repositories.Abstraction;
using BurgerApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation;
using BurgerApp.Services;
using BurgerApp.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        /// <summary>
        /// Injects the application's services into the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IBurgerService, BurgerService>();
            services.AddTransient<ILocationService, LocationService>();
        }

        /// <summary>
        /// Injects the application's repositories into the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the repositories to.</param>
        public static void InjectRepositories(this IServiceCollection services)
        {
            // StaticDb
            //services.AddTransient<IOrderRepository, OrderRepository>();
            //services.AddTransient<IBurgerRepository, BurgerRepository>();
            //services.AddTransient<ILocationRepository, LocationRepository>();

            // Entity Framework
            services.AddTransient<IOrderRepository, OrderRepositoryEntity>();
            services.AddTransient<IBurgerRepository, BurgerRepositoryEntity>();
            services.AddTransient<ILocationRepository, LocationRepositoryEntity>();
        }

        /// <summary>
        /// Injects the application's DbContext into the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the DbContext to.</param>
        /// <param name="connectionString">The connection string to the database.</param>
        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BurgerAppDbContext>(options =>
                options.UseSqlServer(connectionString)
            );
        }
    }
}