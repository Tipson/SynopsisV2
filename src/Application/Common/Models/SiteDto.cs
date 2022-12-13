using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Mappings;
using SynopsisV2.Domain.Entities;

namespace SynopsisV2.Application.Common.Models;

public class SiteDto : IMapFrom<Site>
{
    public int SpeakerId { get; set; }
    public string Url { get; set; }
    public string CompanyName { get; set; }
    public string CompanyNameRu { get; set; }
    public string CompanyNameEn { get; set; }
}