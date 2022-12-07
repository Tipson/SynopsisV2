namespace Synopsis.Infrastructure.DbContext.Entities;

public partial class Partner
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Logo { get; set; }

    public int? Type { get; set; }
    public int? SynopsisType { get; }

    public int? Important { get; set; }

    public int? IsShow { get; set; }

    public string? Url { get; set; }
    
    public IEnumerable<Speaker> Speakers { get; set; }

    public Partner(
        string name,
        string logo,
        int type,
        int synopsisType,
        int important,
        int isShow,
        string url
    )
    {
        Name = name;
        Logo = logo;
        Type = type;
        SynopsisType = synopsisType;
        Important = important;
        IsShow = isShow;
        Url = url;
    }

    public Partner()
    {
        
    }
}

