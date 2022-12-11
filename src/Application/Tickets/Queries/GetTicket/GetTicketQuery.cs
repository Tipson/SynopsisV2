using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Tickets.Queries.GetTicket;

public record GetTicketQuery(int Id) : IRequest<TicketDto>;

public class GetTicketCommandHandler : IRequestHandler<GetTicketQuery, TicketDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetTicketCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<TicketDto> Handle(GetTicketQuery request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Tickets
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);
        
        return _mapper.Map<TicketDto>(row);    
    }
}