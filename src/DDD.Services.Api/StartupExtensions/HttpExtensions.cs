using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace DDD.Services.Api.StartupExtensions
{
    public static class HttpExtensions
    {
        public static IServiceCollection AddCustomizedHttp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("HttpServerA", c =>
            {
                c.BaseAddress = new Uri(configuration.GetSection("HttpServerA").Value);
            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

            return services;
        }
    }
}
