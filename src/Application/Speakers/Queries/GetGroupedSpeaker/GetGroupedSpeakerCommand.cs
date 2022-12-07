using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using Synopsis.Models;
using Synopsis.Models.Speakers;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Speakers.Commands.CreateSpeaker;
using SynopsisV2.Domain.Enums;

namespace SynopsisV2.Application.Speakers.Queries.GetGroupedSpeaker;

public record GetGroupedSpeakerCommand(
    SynopsisVersionType SynopsisVersion, 
    string Lang) : IRequest<SpeakersGroupedCollection>;


public class GetGroupedSpeakerCommandHandler : IRequestHandler<GetGroupedSpeakerCommand, SpeakersGroupedCollection>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGroupedSpeakerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<SpeakersGroupedCollection> Handle(GetGroupedSpeakerCommand request, CancellationToken cancellationToken)
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

        var models = _mapper.Map<List<Common.Models.SpeakerDto>>(rows);

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

            model.TypeName = model.Type;
        }
        

        var speakersGroupByType = models.GroupBy(m => m.Type).ToList();

        return new SpeakersGroupedCollection {
            Speakers = speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.Speaker)?.Union(speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.HostSpeaker)?.ToList() ?? (IEnumerable<SpeakersGroupedCollection>)new List<Speaker>()).ToList(),
            Hosts = speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.Host)?.Union(speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.HostSpeaker)?.ToList() ?? (IEnumerable<SpeakersGroupedCollection>)new List<Speaker>()).ToList(),
            Favorites = models.Where(r=>r.IsFavorite).ToList(),
            
            // Speakers = speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.Speaker)?.ToList(),
            // Hosts = speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.Host)?.ToList(),
            // HostsSpeakers = speakersGroupByType.SingleOrDefault(s => s.Key == SpeakerType.HostSpeaker)?.ToList()
        };
        
    }
}