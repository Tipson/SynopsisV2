using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Mappings;
using SynopsisV2.Domain.Entities;

namespace SynopsisV2.Application.Common.Models;

public class FeedbackDto : IMapFrom<Feedback>
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Body { get; set; }
    public DateTimeOffset DateTimeSend { get; set; }
}