using DDD.Infra.CrossCutting.Identity.Data;
using DDD.Infra.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDD.Services.Api.StartupExtensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddCustomizedDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddDbContext<AuthDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                // Configuring it to throw an exception when a query is evaluated client side
                // This is no longer logged in Entity Framework Core 3.0.
                // options.ConfigureWarnings(warnings =>
                // {
                //     warnings.Throw(RelationalEventId.QueryClientEvaluationWarning);
                // });
                if (!env.IsProduction())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });

            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                if (!env.IsProduction())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });

            services.AddDbContext<EventStoreSqlContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                if (!env.IsProduction())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });

            return services;
        }
    }
}
