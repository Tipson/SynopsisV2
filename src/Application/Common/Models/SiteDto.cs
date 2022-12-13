using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Mappings;

namespace SynopsisV2.Application.Common.Models;

public class SiteDto : IMapFrom<Site>
{
    public int SpeakerId { get; set; }
    public SpeakerDto Speaker { get; set; }
    public string CompanyNameEn { get; set; }
    public string CompanyNameRu { get; set; }
    public string Link { get; set; }
}