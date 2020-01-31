namespace Emailer.Settings
{
    public class HardCodedEmailSettingsProvider : IEmailSettingsProvider
    {
        public string SmtpServer => "mailhog";

        public int SmtpPort => 1025;

        public bool SmtpEnableSsl => false;

        public string SmtpUsername => "whatever";

        public string SmtpPassword => "doesnotmatter";

        public string EmailFromAddress => "someone@example.com";
        public bool IsHtmlEnabledOnMessage => true;
    }
}
