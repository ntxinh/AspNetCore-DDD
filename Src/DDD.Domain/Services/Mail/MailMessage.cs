using System.Collections.Generic;

namespace DDD.Domain.Services.Mail
{
    public class MailMessage
    {
        public MailAddress From { get; set; }
        public List<MailAddress> To { get; set; }
        public List<MailAddress> Cc { get; set; }
        public List<MailAddress> Bcc { get; set; }
        public List<MailAttachment> Attachments { get; set; }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
        public string TemplateId { get; set; }
        public object DynamicTemplateData { get; set; }
    }
}
