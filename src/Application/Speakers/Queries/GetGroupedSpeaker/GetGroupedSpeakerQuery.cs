using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using Synopsis.Models;
using Synopsis.Models.Speakers;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Speakers.Commands.CreateSpeaker;
using SynopsisV2.Domain.Enums;
using Speaker = SynopsisV2.Domain.Entities.Speaker;

namespace SynopsisV2.Application.Speakers.Queries.GetGroupedSpeaker;

public record GetGroupedSpeakerQuery(
    SynopsisVersionType SynopsisVersion, 
    string Lang) : IRequest<SpeakersGroupedCollection>;


public class GetGroupedSpeakerQueryHandler : IRequestHandler<GetGroupedSpeakerQuery, SpeakersGroupedCollection>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGroupedSpeakerQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<SpeakersGroupedCollection> Handle(GetGroupedSpeakerQuery request, CancellationToken cancellationToken)
    {
        var rows = await _dbContext.Speakers
            .Include(r => r.Partner)
            .Include(r=>r.Logos)
            .Include(r=>r.Sites)
            .AsNoTracking()
            .Where(r => r.IsShow == 1 )
            .Where(r => r.SynopsisType == (int)request.SynopsisVersion)
            .OrderByDescending(r => r.Important)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var models = _mapper.Map<List<SpeakerDto>>(rows);

        foreach (var model in models)
        {
            if (request.Lang == "ru")
            {
                model.Position = model.PositionRu;
                model.Name = model.NameRu;
            }
            else
            {
                model.Position = model.PositionEn;
                model.Name = model.NameEn;
            }

            model.TypeName = model.Type.ToString();
        }
        

        var speakersGroupByType = models.GroupBy(m => m.Type).ToList();

        return new SpeakersGroupedCollection {
            Speakers = speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.Speaker)?.Union(speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.HostSpeaker)?.ToList() ?? new List<SpeakerDto>()).ToList(),
            Hosts = speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.Host)?.Union(speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.HostSpeaker)?.ToList() ?? new List<SpeakerDto>()).ToList(),
            Favorites = models.Where(r=>r.IsFavorite).ToList(),
            
            // Speakers = speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.Speaker)?.ToList(),
            // Hosts = speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.Host)?.ToList(),
            // HostsSpeakers = speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.HostSpeaker)?.ToList()
        };
        
    }
}