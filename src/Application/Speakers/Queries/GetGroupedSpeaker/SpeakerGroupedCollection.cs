using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Speakers.Queries.GetGroupedSpeaker;

public class SpeakersGroupedCollection
{
    public List<Common.Models.SpeakerDto>? Hosts { get; init; }
    public List<Common.Models.SpeakerDto>? Speakers { get; init; }
    public List<Common.Models.SpeakerDto> Favorites { get; init; }
}