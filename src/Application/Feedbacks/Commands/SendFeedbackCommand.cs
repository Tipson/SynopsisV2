using MediatR;
using Microsoft.Extensions.Options;
using Synopsis.Helpers;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Configuration;
using SynopsisV2.Domain.Entities;

namespace SynopsisV2.Application.Feedbacks.Commands;

public record SendFeedbackCommand(string Email, string Body) : IRequest<Unit>;

public class SendFeedbackCommandHandler : IRequestHandler<SendFeedbackCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISmtpService _smtpService;
    private readonly IOptions<ManagerSmtpSecrets> _smtpSecrets;

    public SendFeedbackCommandHandler(IApplicationDbContext dbContext, ISmtpService smtpService, IOptions<ManagerSmtpSecrets> smtpSecrets)
    {
        _dbContext = dbContext;
        _smtpService = smtpService;
        _smtpSecrets = smtpSecrets;
    }
    
    public async Task<Unit> Handle(SendFeedbackCommand request, CancellationToken cancellationToken)
    {
        var smtpCommand = new SendSmtpCommand( "Synopsis Feedback user answer", HtmlHelper.GetFeedbackHtmlMessage(request.Email, request.Body), _smtpSecrets.Value.ManagerEmail);
        await _smtpService.SendAsync(smtpCommand, cancellationToken);
        var row = new Feedback(
            request.Email,
            request.Body);
        await _dbContext.Feedbacks
            .AddAsync(row, cancellationToken)
            .ConfigureAwait(false);
        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
        return Unit.Value;
    }
}