using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Feedbacks.Queries.SendFeedback;

public record SendFeedbackCommand : IRequest<FeedbackDto>
{
    public string Email { get; set; }
    public string Body { get; set; }
}

public class SendFeedbackCommandHandler : IRequestHandler<SendFeedbackCommand, FeedbackDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ISmtpService _smtpService;

    public SendFeedbackCommandHandler(IApplicationDbContext dbContext, IMapper mapper, ISmtpService smtpService, IOptions<SmtpSecrets> smtpSecrets)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _smtpService = smtpService;
    }
    public async Task<FeedbackDto> Handle(SendFeedbackCommand request, CancellationToken cancellationToken)
    {
        var smtpCommand = new SendSmtpCommand.SendSmtpCommand( "Synopsis Feedback user answer", HtmlHelper.GetFeedbackHtmlMessage(request.Email, request.Body), _smtpSecrets.Value.ManagerEmail);
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
        
        return null; //Todo
    }
}