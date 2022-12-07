using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Models;
using Speaker = SynopsisV2.Application.Common.Models.Speaker;

namespace SynopsisV2.Application.Speakers.Queries.GetGroupedSpeaker;

public class SpeakersGroupedCollection
{
    public List<Speaker>? Hosts { get; init; }
    public List<Speaker>? Speakers { get; init; }
    public List<Speaker> Favorites { get; init; }
}