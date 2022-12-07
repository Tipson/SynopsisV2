using AutoMapper;
using MediatR;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Tickets.Commands.CreateTicket;
public record CreateTicketCommand(
    int Id,
    string Mail,
    int Type,
    string Code,
    string Telegram,
    string WalletBsc,
    string Login,
    string Name) : IRequest<TicketDto>;

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, TicketDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateTicketCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<TicketDto> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var row = new Ticket(
            request.Mail,
            request.Type,
            request.Code,
            request.Telegram,
            request.WalletBsc,
            request.Login,
            request.Name);
        
        await _dbContext.Tickets
            .AddAsync(row, cancellationToken)
            .ConfigureAwait(false);
        await _dbContext
                
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
        
        return _mapper.Map<TicketDto>(row);    
    }
}