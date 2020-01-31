using Emailer.Settings;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Emailer
{
    public class EmailerClient
    {
        private readonly IEmailSettingsProvider _settingsProvider;

        public EmailerClient(IEmailSettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public void SendGreetingEmailTo(string emailAddress)
        {
            var emailClient = ConfigureEmailClient();
            var mail = ComposeEmail(emailAddress);            

            emailClient.Send(mail);
        }

        private SmtpClient ConfigureEmailClient()
        {
            var emailClient = new SmtpClient(_settingsProvider.SmtpServer);

            emailClient.Port = _settingsProvider.SmtpPort;
            emailClient.EnableSsl = _settingsProvider.SmtpEnableSsl;
            emailClient.Credentials = new NetworkCredential(
                _settingsProvider.SmtpUsername,
                _settingsProvider.SmtpPassword);

            return emailClient;
        }

        private MailMessage ComposeEmail(string emailAddress)
        {
            var mail = new MailMessage();

            mail.From = new MailAddress(_settingsProvider.EmailFromAddress);
            mail.To.Add(emailAddress);
            mail.Subject = "Greetings!";
            mail.Body = "<h1>Hi</h1>This is a <b>greetings message</b>.";
            mail.IsBodyHtml = _settingsProvider.IsHtmlEnabledOnMessage;

            AddAttachement(mail);

            return mail;
        }

        private void AddAttachement(MailMessage mail)
        {
            var attachment = Attachment.CreateAttachmentFromString(
                "contents of the attachement",
                MediaTypeNames.Text.Plain);

            mail.Attachments.Add(attachment);
        }
    }
}