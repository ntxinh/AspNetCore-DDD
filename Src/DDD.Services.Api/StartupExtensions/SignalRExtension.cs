using System;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Services.Api.StartupExtensions;

public static class SignalRExtension
{
    public static IServiceCollection AddCustomizedSignalR(this IServiceCollection services)
    {
        services.AddSignalR(options => {
            options.ClientTimeoutInterval = TimeSpan.FromMinutes(30);
            options.KeepAliveInterval = TimeSpan.FromMinutes(15);
        });
        return services;
    }

    public static IApplicationBuilder UseCustomizedSignalR(this IApplicationBuilder app)
    {
        return app;
    }
}
