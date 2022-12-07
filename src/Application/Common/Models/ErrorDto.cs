using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Mappings;

namespace SynopsisV2.Application.Common.Models;

public class ErrorDto : IMapFrom<Error>
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Message { get; set; }
    public string Controller { get; set; }
    public string Method { get; set; }
    public string Source { get; set; }
}