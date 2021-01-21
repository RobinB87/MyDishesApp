using Microsoft.Extensions.DependencyInjection;

namespace MyDishesApp.WebApi.Extensions
{
    public class IServiceCollectionExtensions
    {
        public static void AddAssembliesToAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DossierMapping).Assembly, typeof(FSDataObjectDtoMapping).Assembly, typeof(SignalDataAccessMapping).Assembly);
        }
    }
}
