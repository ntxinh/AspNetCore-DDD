using DDD.Domain.Services.Mail;

namespace DDD.Domain.Services
{
    public interface IMailService
    {
        void SendMail(MailMessage mailMessage);
    }
}
