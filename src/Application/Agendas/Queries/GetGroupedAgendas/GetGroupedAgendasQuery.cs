using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using SynopsisV2.Application.Agendas.Queries.GetListAgendas;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Domain.Entities;
using SynopsisV2.Domain.Enums;

namespace SynopsisV2.Application.Agendas.Queries.GetGroupedAgendas;

public record GetGroupedAgendasQuery(SynopsisVersionType SynopsisVersionType,
    string Lang) : IRequest<AgendasGroupedCollection>;

public class GetGroupedAgendasQueryHandler : IRequestHandler<GetGroupedAgendasQuery, AgendasGroupedCollection>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGroupedAgendasQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<AgendasGroupedCollection> Handle(GetGroupedAgendasQuery request, CancellationToken cancellationToken)
    {
        var rows = await _dbContext.Agendas
            .Include(r => r.Speakers)
            .AsNoTracking()
            .Where(r => r.SynopsisType == request.SynopsisVersionType.ToString())
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        
        var models = _mapper.Map<List<Agenda>>(rows);
        
        foreach (var model in models)
        {
            if (request.Lang == "ru")
            {
                model.Topic = model.TopicRu;
                foreach (var speaker in model.Speakers)
                {
                    speaker.Name = speaker.NameRu;
                }
            }
            else
            {
                model.Topic = model.TopicEn;
                foreach (var speaker in model.Speakers)
                {
                    speaker.Name = speaker.NameEn;
                }            
            }
        }

        var agendasGroupedByDay = models.GroupBy(r => new DateTime(r.StartTime.Year, r.StartTime.Month, r.StartTime.Day));
        return new AgendasGroupedCollection
        {
            AgendasGrouped = agendasGroupedByDay.Select(a => new AgendasGroupedCollection.AgendaGroupedItem(a.Key, a.ToList())) //todo Paramert type
        };    
    }
}