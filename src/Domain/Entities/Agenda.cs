using Synopsis.Infrastructure.DbContext.Entities;

namespace SynopsisV2.Domain.Entities;

public class Agenda
{
    public Guid Id { get; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public string TopicRu { get; set; }
    public string TopicEn { get; set; }
    public string Topic { get; set; }
    public string SynopsisType { get; }
    public List<Speaker> Speakers { get; set; }

    public Agenda(DateTimeOffset startTime, DateTimeOffset endTime, string topicEn, string topicRu, string synopsisType)
    {
        Id = Guid.NewGuid();
        StartTime = startTime;
        EndTime = endTime;
        TopicEn = topicEn;
        TopicRu = topicRu;
        SynopsisType = synopsisType;
    }
}