using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Mappings;
using SynopsisV2.Domain.Entities;

namespace SynopsisV2.Application.Common.Models;

public class LogoDto : IMapFrom<Logo>
{
    public int SpeakerId { get; set; }
    public string Path { get; set; }
}