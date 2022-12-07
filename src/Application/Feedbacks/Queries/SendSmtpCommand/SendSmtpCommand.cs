using MailKit.Net.Smtp;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;

namespace SynopsisV2.Application.Feedbacks.Queries.SendSmtpCommand;

public record SendSmtpCommand(string Subject, string Body, string Recipient) : IRequest<Unit>;

public class SendSmtpCommandHandler : IRequestHandler<SendSmtpCommand, Unit>
{
    private readonly IOptions<SmtpSecrets> _smtpSecrets;
    private readonly MailboxAddress _senderEmail;
    
    public SendSmtpCommandHandler(IOptions<SmtpSecrets> smtpSecrets)
    {
        _smtpSecrets = smtpSecrets;
        _senderEmail = new MailboxAddress(smtpSecrets.Value.Name, smtpSecrets.Value.Email);
    }
    
    public async Task<Unit> Handle(SendSmtpCommand request, CancellationToken cancellationToken)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(_senderEmail);
        emailMessage.To.Add(new MailboxAddress("", request.Recipient));
        emailMessage.Subject = request.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = request.Body
        };
        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpSecrets.Value.Host, _smtpSecrets.Value.Port, _smtpSecrets.Value.UseSsl, cancellationToken);
        await client.AuthenticateAsync(_smtpSecrets.Value.Login, _smtpSecrets.Value.Password, cancellationToken);
        await client.SendAsync(emailMessage, cancellationToken);

        await client.DisconnectAsync(true, cancellationToken);    
    }
}