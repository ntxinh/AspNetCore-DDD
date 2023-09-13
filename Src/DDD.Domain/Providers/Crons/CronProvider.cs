using System.Collections.Generic;
using System.Threading.Tasks;
using Quartz;

namespace DDD.Domain.Providers.Crons;

public class CronProvider : ICronProvider
{
    private readonly ISchedulerFactory _schedulerFactory;
    public CronProvider(ISchedulerFactory schedulerFactory)
    {
        _schedulerFactory = schedulerFactory;
    }

    public async Task NotifyInactiveUser(NotifyInactiveUserConsumerModel payload)
    {
        IScheduler sched = await _schedulerFactory.GetScheduler();

        IDictionary<string, object> jobData = new Dictionary<string, object>
        {
            { nameof(NotifyInactiveUserConsumerModel.Data), payload.Data },
            { nameof(NotifyInactiveUserConsumerModel.TenantId), payload.TenantId },
            { nameof(NotifyInactiveUserConsumerModel.UserId), payload.UserId },
        };

        var job = JobBuilder.Create<NotifyInactiveUserJob>()
            .WithIdentity(nameof(NotifyInactiveUserJob))
            .Build();

        var replace = true;
        var durable = true;
        await sched.AddJob(job, replace, durable);

        await sched.TriggerJob(new JobKey(nameof(NotifyInactiveUserJob)), new JobDataMap(jobData));
    }
}
