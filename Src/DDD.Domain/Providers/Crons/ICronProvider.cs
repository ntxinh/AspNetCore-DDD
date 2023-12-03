using System.Collections.Generic;
using System.Threading.Tasks;

using Quartz;

namespace DDD.Domain.Providers.Crons;

public interface ICronProvider
{
    // Fire and Forget, One-Off Job
    // https://www.quartz-scheduler.net/documentation/quartz-3.x/how-tos/one-off-job.html
    // Task NotifyInactiveUser(NotifyInactiveUserConsumerModel payload);
    Task OneOffJob<T>(IDictionary<string, object> jobData)
        where T : IJob;
}
