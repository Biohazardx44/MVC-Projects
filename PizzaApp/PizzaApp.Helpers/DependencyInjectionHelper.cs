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
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPizzaService, PizzaService>();
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            //static db
            services.AddTransient<IRepository<Order>, OrderRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IPizzaRepository, PizzaRepository>();

            // entity framework
            //services.AddTransient<IRepository<Order>, OrderRepositoryEntity>();
            //services.AddTransient<IRepository<User>, UserRepositoryEntity>();
            //services.AddTransient<IPizzaRepository, PizzaRepositoryEntity>();
        }

        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PizzaAppDbContext>(options =>
                options.UseSqlServer(connectionString)
            );
        }
    }
}