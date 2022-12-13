using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Tickets.Commands.UpdateTicket;

public record UpdateTicketCommand(
    int Id,
    string Telegram,
    string WalletBsc,
    string Name) : IRequest<TicketDto>;

public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, TicketDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateTicketCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<TicketDto> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Tickets
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (row is null)
        {
            throw new InvalidOperationException($"The Ticket with id = {request.Id} not found");
        }

        row.Name = request.Name;
        row.Telegram = request.Telegram;
        row.WalletBsc = request.WalletBsc;

        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
        return _mapper.Map<TicketDto>(row);    }
}