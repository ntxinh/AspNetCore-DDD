using System.Threading.Tasks;

namespace DDD.Infra.CrossCutting.Identity.Services;

public class AuthSMSMessageSender : ISmsSender
{
    public Task SendSmsAsync(string number, string message)
    {
        // Plug in your SMS service here to send a text message.
        return Task.FromResult(0);
    }
}
