using System.Threading.Tasks;

namespace DDD.Domain.Providers.Mail
{
    public interface IMailProvider
    {
        Task Send(MailMessage message);
    }
}
