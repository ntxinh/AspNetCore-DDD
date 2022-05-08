using System.Threading.Tasks;

namespace DDD.Domain.Services.Mail
{
    public interface IMailService
    {
        Task Send(MailMessage message);
    }
}
