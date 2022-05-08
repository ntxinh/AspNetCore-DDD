using DDD.Application.Interfaces;
using DDD.Application.Services;
using DDD.Domain.CommandHandlers;
using DDD.Domain.Commands;
using DDD.Domain.Core.Bus;
using DDD.Domain.Core.Events;
using DDD.Domain.Core.Notifications;
using DDD.Domain.EventHandlers;
using DDD.Domain.Events;
using DDD.Domain.Interfaces;
using DDD.Domain.Services;
using DDD.Domain.Services.Http;
using DDD.Domain.Services.Mail;
using DDD.Infra.CrossCutting.Bus;
using DDD.Infra.CrossCutting.Identity.Authorization;
using DDD.Infra.CrossCutting.Identity.Models;
using DDD.Infra.CrossCutting.Identity.Services;
using DDD.Infra.Data.EventSourcing;
using DDD.Infra.Data.Repository;
using DDD.Infra.Data.Repository.EventSourcing;
using DDD.Infra.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddHttpContextAccessor();
            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Application
            services.AddScoped<ICustomerAppService, CustomerAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, bool>, CustomerCommandHandler>();

            // Domain - 3rd parties
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IMailService, MailService>();

            // Infra - Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();

            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
            services.AddSingleton<IJwtFactory, JwtFactory>();
        }
    }
}
