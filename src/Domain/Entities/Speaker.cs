using Synopsis.Infrastructure.DbContext.Entities;

namespace SynopsisV2.Domain.Entities;

public partial class Speaker
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? NameEn { get; set; }

    public string? NameRu { get; set; }

    public string? PositionEn { get; set; }

    public string? PositionRu { get; set; }

    public string? Photo { get; set; }

    public int? Type { get; set; }
    public int? SynopsisType { get; set; }
    public int? Important { get; set; }

    public int? IsShow { get; set; }

    public int? IsCommission { get; set; }
    
    public  int? IsFavorite { get; set; } 
    
    public string? BiographyRu { get; set; }
    public string? BiographyEn { get; set; }
    public string? Twitter { get; set; }
    public string? Medium { get; set; }
    public string? Linkedin { get; set; }

    public int? PartnerId { get; set; }
    public List<Agenda> Agendas { get; set; }
    public List<Site> Sites { get; set; }
    public List<Logo> Logos { get; set; }
    public Partner? Partner { get; set; }

    public Speaker(
        string nameRu,
        string nameEn,
        string positionEn,
        string positionRu,
        string photo,
        int type,
        int synopsisType,
        int important,
        int isShow,
        int isCommission,
        int isFavorite,
        string? biographyRu,
        string? biographyEn,
        string? twitter,
        string? medium,
        string? linkedin,
        int? partnerId
        )
    {
        NameRu = nameRu;
        NameEn = nameEn;
        PositionEn = positionEn;
        PositionRu = positionRu;
        Photo = photo;
        Type = type;
        SynopsisType = synopsisType;
        Important = important;
        IsShow = isShow;
        IsCommission = isCommission;
        IsFavorite = isFavorite;
        BiographyEn = biographyEn;
        BiographyRu = biographyRu;
        Twitter = twitter;
        Medium = medium;
        Linkedin = linkedin;
        PartnerId = partnerId;
    }

    public Speaker()
    {
        
    }
}
