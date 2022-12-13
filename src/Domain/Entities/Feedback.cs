namespace SynopsisV2.Domain.Entities;

public class Feedback
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Body { get; set; }
    public DateTimeOffset DateTimeSend { get; set; }
}