using System;
using AutoMapper;
using DDD.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Services.Api.Configurations
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //services.AddAutoMapper(typeof(Startup));
            //services.AddAutoMapper(Assembly.GetAssembly(this.GetType()));
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

            // Registering Mappings automatically only works if the 
            // Automapper Profile classes are in ASP.NET project
            //AutoMapperConfig.RegisterMappings();
        }
    }
}
