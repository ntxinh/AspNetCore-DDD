using System.Reflection;
using System.Text.Json.Serialization;

using DDD.Domain.Providers.Hubs;
using DDD.Infra.CrossCutting.IoC;
using DDD.Services.Api.Configurations;
using DDD.Services.Api.StartupExtensions;

using MediatR;

using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("x-api-version"),
        new MediaTypeApiVersionReader("x-api-version"));
});

// Add ApiExplorer to discover versions
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();

// ----- Swagger UI -----
builder.Services.AddCustomizedSwagger(builder.Environment);

// ----- Health check -----
builder.Services.AddCustomizedHealthCheck(builder.Configuration, builder.Environment);
// END: Custom services

var app = builder.Build();

// Configure the HTTP request pipeline.

// START: Custom middlewares

if (app.Environment.IsDevelopment())
{
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

app.Run();
