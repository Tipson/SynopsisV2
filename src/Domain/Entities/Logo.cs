using Synopsis.Infrastructure.DbContext.Entities;

namespace SynopsisV2.Domain.Entities;

public class Logo
{
    public int SpeakerId { get; set; }
    public Speaker Speaker { get; set; }
    public string LogoPath { get; set; }

    public Logo(
        int speakerId,
        string logo)
    {
        SpeakerId = speakerId;
        LogoPath = logo;
    }
}