using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

// using SendGrid;
// using SendGrid.Helpers.Mail;

namespace DDD.Domain.Services.Mail
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
//         private readonly string devOpsEmail;
//         private readonly ISendGridClient _sendGridClient;

        public MailService(IConfiguration configuration/*, ISendGridClient sendGridClient*/)
        {
            _configuration = configuration;
//             _sendGridClient = sendGridClient;
//             devOpsEmail = _configuration.GetSection("DevOpsEmail").Value;
        }

        public async Task Send(MailMessage message)
        {
            throw new System.NotImplementedException();
        }

//         public async Task Send(MailMessage message)
//         {
//             if (message.From is null || message.From.Email is null) return;
//             if (message.To is null || !message.To.Any()) return;

//             var msg = new SendGridMessage
//             {
//                 From = new EmailAddress(message.From.Email, message.From.Name),
//                 Subject = message.Subject,
//                 PlainTextContent = message.PlainTextContent,
//                 TemplateId = message.TemplateId,
//             };
//             if (message.DynamicTemplateData != null)
//             {
//                 msg.SetTemplateData(message.DynamicTemplateData);
//             }
//             if (message.Cc != null && message.Cc.Any())
//             {
//                 msg.AddCcs(message.Cc.Select(x => new EmailAddress(x.Email, x.Name)).ToList());
//             }
//             if (message.Bcc != null && message.Bcc.Any())
//             {
//                 msg.AddBccs(message.Bcc.Select(x => new EmailAddress(x.Email, x.Name)).ToList());
//             }
//             msg.AddTos(message.To.Select(x => new EmailAddress(x.Email, x.Name)).ToList());
//             msg.AddBcc(devOpsEmail);
//             var response = await _sendGridClient.SendEmailAsync(msg);
//         }
    }
}
