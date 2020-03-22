using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Services.Api.StartupExtensions
{
    public static class HttpExtensions
    {
        public static IServiceCollection AddCustomizedHttp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("HttpServerA", c =>
            {
                c.BaseAddress = new Uri(configuration.GetSection("HttpServerA").Value);
            });

            return services;
        }
    }
}
