using System.Collections;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Speakers.Queries.GetListSpeakers;

public record SpeakerListDto (IEnumerable<SpeakerDto> Speakers);