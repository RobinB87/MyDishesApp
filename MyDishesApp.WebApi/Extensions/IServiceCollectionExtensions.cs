using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyDishesApp.Service.MappingProfiles;

namespace MyDishesApp.WebApi.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddAssembliesToAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
        }
    }
}