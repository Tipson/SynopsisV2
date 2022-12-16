using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Infrastructure.Configuration;

namespace SynopsisV2.Infrastructure.Services;

public class SmtpService : ISmtpService
{
        private readonly IOptions<SmtpSecrets> _smtpSecrets;
    private readonly MailboxAddress _senderEmail;

    public SmtpService(IOptions<SmtpSecrets> smtpSecrets)
    {
        _smtpSecrets = smtpSecrets;
        _senderEmail = new MailboxAddress(smtpSecrets.Value.Name, smtpSecrets.Value.Email);
    }

    public async Task SendAsync(SendSmtpCommand command, CancellationToken cancellationToken)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(_senderEmail);
        emailMessage.To.Add(new MailboxAddress("", command.Recipient));
        emailMessage.Subject = command.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = command.Body
        };
        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpSecrets.Value.Host, _smtpSecrets.Value.Port, _smtpSecrets.Value.UseSsl, cancellationToken);
        await client.AuthenticateAsync(_smtpSecrets.Value.Login, _smtpSecrets.Value.Password, cancellationToken);
        await client.SendAsync(emailMessage, cancellationToken);

        await client.DisconnectAsync(true, cancellationToken);
    }
}