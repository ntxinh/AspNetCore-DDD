using DDD.Domain.Providers.Webhooks;
using Microsoft.AspNetCore.Hosting;
using Quartz;
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

        // Get JobData
        // var jobData = context.MergedJobDataMap;
        // var tenantId = (int)jobData.Get(nameof(Tenants.TenantId));

        // TODO: Your logic here

        await _webhookProvider.Send($"END CheckInactiveUser, Env {_env.EnvironmentName}");
    }
}
