using System.Collections.Generic;

namespace DDD.Domain.Services.Mail
{
    public class MailMessage
    {
        public MailAddress From { get; set; }
        public IEnumerable<MailAddress> To { get; set; }
        public IEnumerable<MailAddress> Cc { get; set; }
        public IEnumerable<MailAddress> Bcc { get; set; }
        public IEnumerable<Attachment> Attachments { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
