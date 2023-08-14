using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaApp.DataAccess.Data;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation;
using PizzaApp.DataAccess.Repositories.Implementation.StaticDbImplementation;
using PizzaApp.Domain.Models;
using PizzaApp.Services;
using PizzaApp.Services.Abstraction;

namespace PizzaApp.Helpers
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
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPizzaService, PizzaService>();
        }

        /// <summary>
        /// Injects the application's repositories into the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the repositories to.</param>
        public static void InjectRepositories(this IServiceCollection services)
        {
            // StaticDb
            services.AddTransient<IRepository<Order>, OrderRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IPizzaRepository, PizzaRepository>();

            // Entity Framework
            //services.AddTransient<IRepository<Order>, OrderRepositoryEntity>();
            //services.AddTransient<IRepository<User>, UserRepositoryEntity>();
            //services.AddTransient<IPizzaRepository, PizzaRepositoryEntity>();
        }

        /// <summary>
        /// Injects the application's DbContext into the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the DbContext to.</param>
        /// <param name="connectionString">The connection string to the database.</param>
        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PizzaAppDbContext>(options =>
                options.UseSqlServer(connectionString)
            );
        }
    }
}