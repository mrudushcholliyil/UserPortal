using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using UserPortal.Application.Common.Interfaces;
using UserPortal.Infrastructure.Configuration;
using UserPortal.Infrastructure.Logging;
using UserPortal.Infrastructure.Persistence.DbContexts;
using UserPortal.Infrastructure.Persistence.Repositories;
using UserPortal.Infrastructure.Persistence.UnitOfWork;

namespace UserPortal.Infrastructure
{
    /// <summary>
    /// Dependency injection configuration for the infrastructure layer.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<DatabaseSettingsOptions>(configuration.GetSection(DatabaseSettingsOptions.SectionName));

            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<DatabaseSettingsOptions>>().Value.SqlServerConnectionString);
            });

            // Register the generic repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton(typeof(ILoggerService<>), typeof(SerilogLoggerService<>));
            
            return services;
        }
    }
}
