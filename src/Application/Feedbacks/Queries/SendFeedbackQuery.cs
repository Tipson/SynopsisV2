using MediatR;
using Microsoft.Extensions.Options;
using Synopsis.Infrastructure;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Feedbacks.Queries;

public record SendFeedbackQuery(string Email, string Body) : IRequest<Unit>;

public class SendFeedbackQueryHandler : IRequestHandler<SendFeedbackQuery, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISmtpService _smtpService;
    private readonly IOptions<SmtpSecrets> _smtpSecrets;

    public SendFeedbackQueryHandler(IApplicationDbContext dbContext, ISmtpService smtpService, IOptions<SmtpSecrets> smtpSecrets)
    {
        _dbContext = dbContext;
        _smtpService = smtpService;
        _smtpSecrets = smtpSecrets;
    }
    
    public async Task<Unit> Handle(SendFeedbackQuery request, CancellationToken cancellationToken)
    {
        var smtpCommand = new SendSmtpCommand( "Synopsis Feedback user answer", HtmlHelper.GetFeedbackHtmlMessage(request.Email, request.Body), _smtpSecrets.Value.ManagerEmail);
        await _smtpService.SendAsync(smtpCommand, cancellationToken);
        var row = new FeedbackDto(
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