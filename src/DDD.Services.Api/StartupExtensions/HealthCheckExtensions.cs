using DDD.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Services.Api.StartupExtensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddCustomizedHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .AddDbContextCheck<ApplicationDbContext>();
            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint("endpoint1", "/healthz");
            });

            return services;
        }
    }
}
