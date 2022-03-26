using System.Collections.Generic;
using System.Threading.Tasks;

using DDD.Domain.Services.Mail;

namespace DDD.Domain.Services
{
    public interface IMailService
    {
        void SendMail(MailMessage mailMessage);
//         Task Send(MailMessage message);
    }
    
//     public class MailMessage
//     {
//         public MailAddress From { get; set; }
//         public List<MailAddress> To { get; set; }
//         public List<MailAddress> Cc { get; set; }
//         public List<MailAddress> Bcc { get; set; }
//         public string Subject { get; set; }
//         public string PlainTextContent { get; set; }
//         public string TemplateId { get; set; }
//         public object DynamicTemplateData { get; set; }
//     }

//     public class MailAddress
//     {
//         public string Email { get; set; }
//         public string Name { get; set; }
//     }
}
