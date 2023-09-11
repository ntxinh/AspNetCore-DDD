using System.Threading.Tasks;

namespace DDD.Domain.Providers.Webhooks;

public interface IWebhookProvider
{
    Task Send(string message);
}
