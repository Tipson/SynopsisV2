using System.Collections;
using Synopsis.Infrastructure.DbContext.Entities;

namespace SynopsisV2.Application.Speakers.Queries.GetListSpeakers;

public record SpeakerListDto (IEnumerable Speakers);