using SynopsisV2.Application.Common.Interfaces;

namespace SynopsisV2.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
