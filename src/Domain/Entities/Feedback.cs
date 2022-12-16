namespace SynopsisV2.Domain.Entities;

public class Feedback
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Body { get; set; }
    public DateTimeOffset DateTimeSend { get; set; }

    public Feedback(string email, string body)
    {
        Id = Guid.NewGuid();
        Email = email;
        Body = body;
        DateTimeSend = DateTimeOffset.Now;
    }
}
