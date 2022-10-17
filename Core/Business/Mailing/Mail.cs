using System.Net.Mail;

namespace Core.Business.Mailing
{
    public class Mail
    {
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public string HtmlBody { get; set; }
        public string ToFullName { get; set; }
        public string ToMail { get; set; }
        public AttachmentCollection Attachments { get; set; }
    }
}
