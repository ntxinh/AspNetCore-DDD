using Asp.Versioning;

using Serilog;

using System.Reflection;
using System.Text.Json.Serialization;

using DDD.Domain.Providers.Hubs;
using DDD.Infra.CrossCutting.IoC;
using DDD.Services.Api.Configurations;
using DDD.Services.Api.StartupExtensions;

// using Microsoft.AspNetCore.Mvc.Versioning;

// 1. Setup the Bootstrap logger (logs startup errors)
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting web application...");

    var builder = WebApplication.CreateBuilder(args);

    // 2. Tell the Host to use Serilog
    // This reads the configuration from appsettings.json
    builder.Host.UseSerilog((context, services, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    // START: Variables
    // END: Variables

    // START: Custom services
    // ----- Database -----
    builder.Services.AddCustomizedDatabase(builder.Configuration, builder.Environment);

    // ----- Auth -----
    builder.Services.AddCustomizedAuth(builder.Configuration);

    // ----- Http -----
    builder.Services.AddCustomizedHttp(builder.Configuration);

    // ----- AutoMapper -----
    builder.Services.AddAutoMapperSetup();

    // Adding MediatR for Domain Events and Notifications
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    });

    // ----- Hash -----
    builder.Services.AddCustomizedHash(builder.Configuration);

    // ----- SignalR -----
    builder.Services.AddCustomizedSignalR();

    // ----- Quartz -----
    builder.Services.AddCustomizedQuartz(builder.Configuration);

    // .NET Native DI Abstraction
    NativeInjectorBootStrapper.RegisterServices(builder.Services);

    builder.Services.AddControllers()
        .AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            // x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

    // 1. Add ApiVersioning
    // This registers the core services and returns an IApiVersioningBuilder
    var versioningBuilder = builder.Services.AddApiVersioning(opt =>
    {
        opt.DefaultApiVersion = new ApiVersion(1, 0);
        opt.AssumeDefaultVersionWhenUnspecified = true;
        opt.ReportApiVersions = true;
        opt.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader(),
            new HeaderApiVersionReader("x-api-version"),
            new MediaTypeApiVersionReader("x-api-version"));
    });

    // 2. Add ApiExplorer
    // Chain this directly off the builder created above
    versioningBuilder.AddApiExplorer(setup =>
    {
        setup.GroupNameFormat = "'v'VVV";
        setup.SubstituteApiVersionInUrl = true;
    });

    builder.Services.AddEndpointsApiExplorer();

    // Add services to the container.
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    // ----- Swagger UI -----
    builder.Services.AddCustomizedSwagger(builder.Environment);

    // ----- Health check -----
    builder.Services.AddCustomizedHealthCheck(builder.Configuration, builder.Environment);
    // END: Custom services

    var app = builder.Build();

    // 3. Add Request Logging (Optional but recommended)
    // Logs a neat summary of every HTTP request
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.

    // START: Custom middlewares

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();

        // ----- Error Handling -----
        app.UseCustomizedErrorHandling();
    }

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

    // ----- Controller -----
    app.MapControllers();

    // ----- SignalR -----
    app.MapHub<NotificationHub>($"/hub{HubRoutes.Notification}");

    // ----- Health check -----
    HealthCheckExtension.UseCustomizedHealthCheck(app, builder.Environment);

    // ----- Swagger UI -----
    app.UseCustomizedSwagger(builder.Environment);
    // END: Custom middlewares

    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    // 4. Ensure logs are flushed before exit
    Log.CloseAndFlush();
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
