using AutoMapper;
using MediatR;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using Synopsis.Models;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Speakers.Queries.GetSpeakerAsRow;
using SynopsisV2.Domain.Entities;
using SynopsisV2.Domain.Enums;

namespace SynopsisV2.Application.Agendas.Commands.CreateAgenda;

public record CreateAgendaCommand(
    IEnumerable<GetAsRowSpeakerCommand> Speakers,
    SynopsisVersionType SynopsisType,
    string TopicEn,
    string TopicRu,
    DateTimeOffset StartTime,
    DateTimeOffset EndTime) : IRequest<AgendaDto>;

public class CreateAgendaCommandHandler : IRequestHandler<CreateAgendaCommand, AgendaDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateAgendaCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<AgendaDto> Handle(CreateAgendaCommand request, CancellationToken cancellationToken)
    {
        var speakersRow = request.Speakers.Select( s => _speakersService.GetAsRow(s, cancellationToken).Result).ToList();
        
        var row = new Agenda(
            request.StartTime,
            request.EndTime,
            request.TopicEn,
            request.TopicRu,
            request.SynopsisType.ToString()
        );
        row.Speakers = speakersRow;
        
        await _dbContext.Agendas
            .AddAsync(row, cancellationToken)
            .ConfigureAwait(false);
        
        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
        
        return _mapper.Map<AgendaDto>(row);
    }
}