using Synopsis.Infrastructure.DbContext.Entities;
using Synopsis.Models;
using SynopsisV2.Application.Common.Mappings;
using SynopsisV2.Domain.Entities;
using SynopsisV2.Domain.Enums;

namespace SynopsisV2.Application.Common.Models;

public class AgendaDto : IMapFrom<Agenda>
{
    public Guid Id { get; set; }
    public List<Synopsis.Infrastructure.DbContext.Entities.Speaker> Speakers { get; set; }
    public SynopsisVersionType SynopsisType { get; set; }
    public string Topic { get; set; }
    public string TopicEn { get; set; }
    public string TopicRu { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}