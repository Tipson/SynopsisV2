using Synopsis.Infrastructure.DbContext.Entities;
using Synopsis.Models;
using Synopsis.Models.Partners;
using Synopsis.Models.Speakers;
using SynopsisV2.Application.Common.Mappings;
using SynopsisV2.Domain.Enums;

namespace SynopsisV2.Application.Common.Models;

public class SpeakerDto : IMapFrom<Speaker>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NameEn { get; set; }
    public string NameRu { get; set; }
    public string Position { get; set; }
    public string PositionEn { get; set; }
    public string PositionRu { get; set; }
    public string Photo { get; set; }
    public string Type { get; set; }
    public SynopsisVersionType SynopsisType { get; set; }
    public string TypeName { get; set; }
    public ImportanceType Important { get; set; }
    public bool IsCommission { get; set; }
    public int IsShow { get; set; }
    public bool IsFavorite { get; set; }
    public PartnerItem? Partner { get; set; }
    public string Biography { get; set; }
    public string BiographyEn { get; set; }
    public string BiographyRu { get; set; }
    public string Twitter { get; set; }
    public string Linkedin { get; set; }
    public string Medium { get; set; }
    public List<Site> Sites { get; set; }
    public List<Logo> Logos { get; set; }
}

public class PartnerItem //todo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public string Url { get; set; }
    public int TypeAlias { get; set; }
    public PartnerType Type { get; set; }
    public string TypeName { get; set; }
    public ImportanceType Important { get; set; }
    public int IsShow { get; set; }
}