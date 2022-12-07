namespace SynopsisV2.Application.Speakers.Commands.CreateSpeaker;

public record CreateSiteCommand(
    string Url,
    string CompanyNameEn,
    string CompanyNameRu
);