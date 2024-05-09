using Microsoft.Extensions.DependencyInjection;
using PlaceRentalApp.Application.Services;

namespace PlaceRentalApp.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPlaceService, PlaceSevice>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
