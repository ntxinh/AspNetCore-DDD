using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DDD.Services.Api.StartupExtensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddCustomizedSwagger(this IServiceCollection services, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "ASPNET Core DDD Project",
                        Description = "",
                        Contact = new OpenApiContact { Name = "Xinh Nguyen", Email = "nguyentrucxjnh@gmail.com", Url = new Uri("https://nguyentrucxinh.github.io/") },
                        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://github.com/nguyentrucxinh/AspNetCore-DDD/blob/master/LICENSE") }
                    });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                            // new string[] { }
                        }
                    });
                    // Add custom header request
                    //c.OperationFilter<AddRequiredHeaderParameter>();
                });
            }

            return services;
        }

        public static IApplicationBuilder UseCustomizedSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASPNET Core DDD Project API v1.1");
                });
            }

            return app;
        }
    }
}
