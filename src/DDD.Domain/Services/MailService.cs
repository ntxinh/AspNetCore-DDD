using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDD.Domain.Services
{
    public class MailService : IMailService
    {
        public MailService()
        {
        }

        public void SendMail(string fromEmail, string fromName, string toEmail, string toName, string subject, string plainTextContent, string htmlContent)
        {
            throw new System.NotImplementedException();
        }

        public void SendMail(string fromEmail, string fromName, string toEmail, string toName, string templateId, object dynamicTemplateData)
        {
            throw new System.NotImplementedException();
        }

        public Task SendToMultipleMail(string fromEmail, string fromName, string[] toEmails, string[] toNames, string subject, string plainTextContent, string htmlContents, List<Dictionary<string, string>> substitutions)
        {
            throw new System.NotImplementedException();
        }

        public Task SendToMultipleMail(string fromEmail, string fromName, string[] toEmails, string[] toNames, string templateId, List<object> dynamicTemplateDatas)
        {
            throw new System.NotImplementedException();
        }
    }
}
