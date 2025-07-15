using UserPortal.Application;
using UserPortal.Infrastructure;

namespace UserPortal.Api
{

    /// <summary>
    /// used to register all the dependencies for the API layer.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI()
                    .AddInfrastructureDI(configuration); 

            return services;
        }
    }
}
