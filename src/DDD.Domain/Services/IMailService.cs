using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDD.Domain.Services
{
    public interface IMailService
    {
        void SendMail(string fromEmail, string fromName, string toEmail, string toName, string subject, string plainTextContent, string htmlContent);
        Task SendToMultipleMail(string fromEmail, string fromName, string[] toEmails, string[] toNames, string subject, string plainTextContent, string htmlContents, List<Dictionary<string, string>> substitutions);
        Task SendToMultipleMail(string fromEmail, string fromName, string[] toEmails, string[] toNames, string templateId, List<object> dynamicTemplateDatas);
        void SendMail(string fromEmail, string fromName, string toEmail, string toName, string templateId, object dynamicTemplateData);
    }
}
