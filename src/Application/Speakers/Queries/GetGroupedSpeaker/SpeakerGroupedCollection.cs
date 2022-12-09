using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Speakers.Queries.GetGroupedSpeaker;

public class SpeakersGroupedCollection
{
    public List<SpeakerDto>? Hosts { get; init; }
    public List<SpeakerDto>? Speakers { get; init; }
    public List<SpeakerDto> Favorites { get; init; }
}