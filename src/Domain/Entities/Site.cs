using Synopsis.Infrastructure.DbContext.Entities;

namespace SynopsisV2.Domain.Entities;

public class Site
{
    public int SpeakerId { get; set; }
    public Speaker Speaker { get; set; }
    public string CompanyNameEn { get; set; }
    public string CompanyNameRu { get; set; }
    public string Link { get; set; }
    

    public Site(
        int speakerId,
        string companyNameEn, 
        string companyNameRu, 
        string link)
    {
        SpeakerId = speakerId;
        CompanyNameEn = companyNameEn;
        CompanyNameRu = companyNameRu;
        Link = link;
    }
}