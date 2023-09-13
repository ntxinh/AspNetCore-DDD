using DDD.Domain.Providers.Webhooks;
using Microsoft.AspNetCore.Hosting;
using Quartz;
// using System.Collections.Generic;
// using System.Linq;
using System.Threading.Tasks;

namespace DDD.Domain.Providers.Crons;

[DisallowConcurrentExecution]
public class NotifyInactiveUserJob : IJob
{
    private readonly IWebHostEnvironment _env;
    private readonly IWebhookProvider _webhookProvider;
    public NotifyInactiveUserJob(IWebHostEnvironment env, IWebhookProvider webhookProvider)
    {
        _env = env;
        _webhookProvider = webhookProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _webhookProvider.Send($"START CheckInactiveUser, Env {_env.EnvironmentName}");

        // // Get JobData
        // var jobData = context.MergedJobDataMap;

        // // Validate
        // if (!jobData.ContainsKey(nameof(NotifyInactiveUserConsumerModel.TenantId))
        //     || !jobData.ContainsKey(nameof(NotifyInactiveUserConsumerModel.UserId))
        //     || !jobData.ContainsKey(nameof(NotifyInactiveUserConsumerModel.Data))) return;

        // // Parse data
        // var tenantId = (short)jobData.Get(nameof(NotifyInactiveUserConsumerModel.TenantId));
        // var userId = (int)jobData.Get(nameof(NotifyInactiveUserConsumerModel.UserId));
        // var data = (List<object>)jobData.Get(nameof(NotifyInactiveUserConsumerModel.Data));

        // if (!data.Any() || tenantId <= 0 || userId <= 0) return;

        // TODO: Your logic here

        await _webhookProvider.Send($"END CheckInactiveUser, Env {_env.EnvironmentName}");
    }
}
