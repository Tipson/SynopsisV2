using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Speakers.Queries.GetSpeakerAsRow;

namespace SynopsisV2.Application.Agendas.Commands.UpdateAgenda;

public record UpdateAgendaCommand(
    Guid Id,
    string TopicEn,
    string TopicRu,
    DateTimeOffset Time,
    List<GetAsRowSpeakerCommand> Speakers) : IRequest<AgendaDto>;

public class UpdateAgendaCommandHandler : IRequestHandler<UpdateAgendaCommand, AgendaDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateAgendaCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<AgendaDto> Handle(UpdateAgendaCommand request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Agendas
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);
        if (row is null)
        {
            throw new InvalidOperationException($"The Agenda with id = {request.Id} not found");
        }

        row.TopicEn = request.TopicEn;
        row.TopicRu = request.TopicRu;
        row.StartTime = request.Time;
        row.Speakers = request.Speakers.Select( s => _speakersService.GetAsRow(s, cancellationToken).Result).ToList();
        
        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
        
        return _mapper.Map<AgendaDto>(row);    
    }
}