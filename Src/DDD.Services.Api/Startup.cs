using DDD.Domain.Providers.Hubs;
using DDD.Infra.CrossCutting.IoC;
using DDD.Services.Api.Configurations;
using DDD.Services.Api.StartupExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Text.Json.Serialization;

namespace DDD.Services.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ----- Database -----
            services.AddCustomizedDatabase(Configuration, _env);

            // ----- Auth -----
            services.AddCustomizedAuth(Configuration);

            // ----- Http -----
            services.AddCustomizedHttp(Configuration);

            // ----- AutoMapper -----
            services.AddAutoMapperSetup();

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // ----- Hash -----
            services.AddCustomizedHash(Configuration);

            // ----- SignalR -----
            services.AddCustomizedSignalR();

            // ----- Quartz -----
            services.AddCustomizedQuartz(Configuration);

            // .NET Native DI Abstraction
            RegisterServices(services);

            services.AddControllers()
                .AddJsonOptions(x => {
                    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    // x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
                // Nuget package Microsoft.AspNetCore.Mvc.NewtonsoftJson
                // .AddNewtonsoftJson(
                //     options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                // );

            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                new HeaderApiVersionReader("x-api-version"),
                                                                new MediaTypeApiVersionReader("x-api-version"));
            });

            // Add ApiExplorer to discover versions
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddEndpointsApiExplorer();

            // ----- Swagger UI -----
            services.AddCustomizedSwagger(_env);

            // ----- Health check -----
            services.AddCustomizedHealthCheck(Configuration, _env);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // ----- Error Handling -----
            app.UseCustomizedErrorHandling(_env);

            app.UseRouting();

            // ----- CORS -----
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // ----- Auth -----
            app.UseCustomizedAuth();

            // ----- SignalR -----
            app.UseCustomizedSignalR();

            // ----- Quartz -----
            app.UseCustomizedQuartz();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // ----- SignalR -----
                endpoints.MapHub<NotificationHub>($"/hub{HubRoutes.Notification}");

                // ----- Health check -----
                HealthCheckExtension.UseCustomizedHealthCheck(endpoints, _env);
            });

            // ----- Swagger UI -----
            app.UseCustomizedSwagger(_env);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
