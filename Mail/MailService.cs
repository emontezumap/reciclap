using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;

namespace Mail;

public class MailService : IMailService
{
    private readonly MailSettings mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
    {
        this.mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage();
        //        email.Sender = MailboxAddress.Parse(mailSettings.From);
        email.From.Add(new MailboxAddress("email", mailSettings.From));
        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        email.Subject = mailRequest.Subject;
        var builder = new BodyBuilder();

        if (mailRequest.Attachments != null)
        {
            byte[] fileBytes;
            foreach (var file in mailRequest.Attachments)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }
        }

        builder.HtmlBody = mailRequest.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        smtp.Connect(mailSettings.SmtpServer, mailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(mailSettings.Username, mailSettings.Password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}