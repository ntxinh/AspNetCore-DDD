using System.Threading.Tasks;

namespace DDD.Domain.Providers.Mail
{
    public interface IMailService
    {
        Task Send(MailMessage message);
    }
}
