using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Domain.Entities;

namespace SynopsisV2.Application.Agendas.Queries.GetAgenda;

public record GetAgendaQuery(Guid Id) : IRequest<AgendaDto>;

public class GetAgendaQueryHandler : IRequestHandler<GetAgendaQuery, AgendaDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAgendaQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<AgendaDto> Handle(GetAgendaQuery request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Agendas
            .Include(r=>r.Speakers)
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);
        
        if (row is null)
        {
            throw new InvalidOperationException($"$The Agenda with id = {request.Id} not found");
        }

        return _mapper.Map<AgendaDto>(row);    
    }
}