using DDD.Domain.Providers.Crons;
using Quartz;
using Quartz.AspNetCore;

namespace DDD.Services.Api.StartupExtensions;

public static class QuartzExtensions
{
    public static IServiceCollection AddCustomizedQuartz(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(q =>
        {
            var myTz = "SE Asia Standard Time";
            var tz = TimeZoneInfo.FindSystemTimeZoneById(myTz);

            // Create a "key" for the job
            var jobKey = new JobKey(nameof(NotifyInactiveUserJob));

            // Register the job with the DI container
            q.AddJob<NotifyInactiveUserJob>(opts => opts.WithIdentity(jobKey));

            // Create a trigger for the job
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity($"{nameof(NotifyInactiveUserJob)}-trigger")
                // run at 6:00 PM every weekdays
                .WithCronSchedule("0 0 18 ? * MON,TUE,WED,THU,FRI *", x => x.InTimeZone(tz))
            );
        });

        // ASP.NET Core hosting
        services.AddQuartzServer(options =>
        {
            // when shutting down we want jobs to complete gracefully
            options.WaitForJobsToComplete = true;
        });

        return services;
    }

    public static IApplicationBuilder UseCustomizedQuartz(this IApplicationBuilder app)
    {
        return app;
    }
}
