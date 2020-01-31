namespace Emailer.Settings
{
    public interface IEmailSettingsProvider
    {
        string SmtpServer { get; }
        int SmtpPort { get; }
        bool SmtpEnableSsl { get; }
        string SmtpUsername { get; }
        string SmtpPassword { get; }
        string EmailFromAddress { get; }
        bool IsHtmlEnabledOnMessage { get; }
    }
}
