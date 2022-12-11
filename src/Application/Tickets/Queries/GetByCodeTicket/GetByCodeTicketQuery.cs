using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Tickets.Queries.GetByCodeTicket;

public record GetByCodeTicketQuery(string Code) : IRequest<TicketDto>;

public class GetByCodeTicketCommandHandler : IRequestHandler<GetByCodeTicketQuery, TicketDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetByCodeTicketCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
     
    public async Task<TicketDto> Handle(GetByCodeTicketQuery request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Tickets
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Code == request.Code, cancellationToken)
            .ConfigureAwait(false);
        
        return _mapper.Map<TicketDto>(row);
        
    }
}