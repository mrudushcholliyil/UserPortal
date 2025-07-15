using Microsoft.Extensions.DependencyInjection;

namespace UserPortal.Application
{
    /// <summary>
    /// Dependency injection configuration for the Application layer.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            services.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            return services;
        }
    }
}
