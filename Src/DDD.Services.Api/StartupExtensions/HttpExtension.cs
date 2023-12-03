using DDD.Domain.Providers.Http;

using Polly;

namespace DDD.Services.Api.StartupExtensions;

public static class HttpExtension
{
    public static IServiceCollection AddCustomizedHttp(this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration.GetValue<string>("HttpClients:Foo");

        if (!string.IsNullOrEmpty(url))
        {
            services
                .AddHttpClient("Foo", c =>
                {
                    c.BaseAddress = new Uri(url);
                })
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)))
                .AddTypedClient(c => Refit.RestService.For<IFooClient>(c));
        }

        return services;
    }
}
