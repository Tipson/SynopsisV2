using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;

namespace SynopsisV2.Application.Agendas.Commands.DeleteAgenda;
public record DeleteAgendaCommand(Guid Id) : IRequest<Unit>;

public class DeleteAgendaCommandHandler : IRequestHandler<DeleteAgendaCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;


    public DeleteAgendaCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;

    }
    public async Task<Unit> Handle(DeleteAgendaCommand request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Agendas
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);
        
        if (row is null)
            return Unit.Value;
        
        _dbContext.Agendas
            .Remove(row);
        
        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
        return Unit.Value;
    }
}