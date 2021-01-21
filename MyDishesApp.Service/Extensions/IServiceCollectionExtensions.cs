using Microsoft.Extensions.DependencyInjection;
using MyDishesApp.Repository.Extensions;
using MyDishesApp.Service.Service;
using MyDishesApp.Service.Service.Interfaces;

namespace MyDishesApp.Service.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddServiceLayerWithDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDishService, DishService>();
            services.AddRepositoryLayer(connectionString);
        }
    }
}