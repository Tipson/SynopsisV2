namespace Synopsis.Infrastructure.DbContext.Entities;

public class Logo
{
    public int SpeakerId { get; set; }
    public Speaker Speaker { get; set; }
    public string LogoPath { get; set; }

    public Logo(
        int speakerId,
        string logoPath)
    {
        SpeakerId = speakerId;
        LogoPath = logoPath;
    }
}