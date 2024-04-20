using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using ShopApp.WebUI.SettingsForMail;

namespace ShopApp.WebUI.Services;

public class EmailSenderService : IEmailSender
{
    private readonly ISendGridClient  _sendGridClient;
    private readonly Settings _sendGridSettings;
    public EmailSenderService(ISendGridClient sendGridClient,
    IOptions<Settings> sendGridSettings)
    {
        _sendGridClient = sendGridClient;
        _sendGridSettings = sendGridSettings.Value;
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(_sendGridSettings.FromEmail, _sendGridSettings.EmailName),
            Subject = subject,
            HtmlContent = htmlMessage
        };
	    msg.AddTo(email);
	    await _sendGridClient.SendEmailAsync(msg);
    }
}