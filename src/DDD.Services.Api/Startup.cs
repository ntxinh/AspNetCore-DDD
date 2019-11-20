using DDD.Infra.CrossCutting.Identity.Data;
using DDD.Infra.CrossCutting.IoC;
using DDD.Services.Api.Configurations;
using DDD.Services.Api.StartupExtensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Services.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // ----- Auth -----
            services.AddCustomizedAuth(Configuration);

            // ----- AutoMapper -----
            services.AddAutoMapperSetup();

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            RegisterServices(services);

            // ----- Swagger UI -----
            services.AddCustomizedSwagger();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ----- Error Handling -----
            app.UseCustomizedErrorHandling(env);

            // ----- CORS -----
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting();

            // ----- Auth -----
            app.UseCustomizedAuth();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // ----- Swagger UI -----
            app.UseCustomizedSwagger();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
