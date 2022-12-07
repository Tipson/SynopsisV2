namespace Synopsis.Infrastructure.DbContext.Entities;

public partial class Error
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Message { get; set; }

    public string? Controller { get; set; }

    public string? Method { get; set; }

    public string? Source { get; set; }

    public Error(
        string message,
        string controller,
        string method,
        string source)
    {
        Date = DateTime.UtcNow;
        Message = message;
        Controller = controller;
        Method = method;
        Source = source;
    }

    public Error()
    {
        
    }
}

