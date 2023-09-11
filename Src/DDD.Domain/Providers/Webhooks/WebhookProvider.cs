using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DDD.Domain.Providers.Webhooks;

public class WebhookProvider : IWebhookProvider
{
    private readonly IConfiguration _configuration;

    public WebhookProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Send(string message)
    {
        var client = new HttpClient();
        var uri = _configuration.GetValue<string>("Webhook:Slack");
        if (string.IsNullOrEmpty(uri)) return;
        await client.PostAsJsonAsync(uri, new { text = message });
    }
}
