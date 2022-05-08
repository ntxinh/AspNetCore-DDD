using System;
using DDD.Domain.Services;
using DDD.Domain.Services.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;

namespace DDD.Services.Api.StartupExtensions
{
    public static class HttpExtension
    {
        public static IServiceCollection AddCustomizedHttp(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpClient("Foo", c =>
                {
                    c.BaseAddress = new Uri(configuration.GetValue<string>("HttpClients:Foo"));
                })
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)))
                .AddTypedClient(c => Refit.RestService.For<IFooClient>(c));

            return services;
        }
    }
}
