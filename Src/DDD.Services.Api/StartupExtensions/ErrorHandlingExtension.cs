using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DDD.Services.Api.StartupExtensions
{
    public static class ErrorHandlingExtension
    {
        public static IApplicationBuilder UseCustomizedErrorHandling(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            return app;
        }
    }
}
