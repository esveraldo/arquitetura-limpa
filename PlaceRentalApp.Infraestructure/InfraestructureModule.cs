using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlaceRentalApp.Infraestructure.Persistence;

namespace PlaceRentalApp.Infraestructure
{
    public static class InfraestructureModule
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddData(configuration);

            return services;
        }

        private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PlaceRentalCs");
            services.AddDbContext<PlaceRentalDbContext>(o => o.UseSqlServer(connectionString));

            return services;
        }
    }
}
