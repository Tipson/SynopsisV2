using AutoMapper;
using MediatR;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using Synopsis.Models;
using Synopsis.Models.Speakers;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Domain.Entities;
using SynopsisV2.Domain.Enums;

namespace SynopsisV2.Application.Speakers.Commands.CreateSpeaker;

public record CreateSpeakerCommand(

    string NameRu,
    string NameEn,
    string PositionRu,
    string PositionEn,
    string Photo,
    SpeakerType Type,
    SynopsisVersionType SynopsisType,
    ImportanceType Importance,
    string? BiographyRu,
    string? BiographyEn,
    string? Twitter,
    string? Medium,
    string? Linkedin,
    List<CreateSiteCommand> Sites,
    List<CreateLogoCommand> Logos,
    bool IsShow = true,
    bool CommissionMember = false,
    bool IsFavorite = false,
    int? PartnerId = null) : IRequest<SpeakerDto>;

public class CreateSpeakerCommandHandler : IRequestHandler<CreateSpeakerCommand, SpeakerDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public CreateSpeakerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<SpeakerDto> Handle(CreateSpeakerCommand request, CancellationToken cancellationToken)
    {
        var row = new Speaker(
            request.NameEn,
            request.NameRu,
            request.PositionEn,
            request.PositionRu,
            request.Photo,
            (int)request.Type,
            (int)request.SynopsisType,
            (int)request.Importance,
            request.IsShow ? 1 : 0,
            request.CommissionMember ? 1 : 0,
            request.IsFavorite ? 1 : 0,
            request.BiographyRu,
            request.BiographyEn,
            request.Twitter,
            request.Medium,
            request.Linkedin,
            request.PartnerId 
        );
        
        var speakerRow = await _dbContext.Speakers
            .AddAsync(row, cancellationToken)
            .ConfigureAwait(false);

        speakerRow.Entity.Sites = request.Sites.Select(e => new Site(speakerRow.Entity.Id, e.CompanyNameEn, e.CompanyNameRu, e.Url)).ToList();
        speakerRow.Entity.Logos = request.Logos.Select(e => new Logo(speakerRow.Entity.Id, e.Path)).ToList();

        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<SpeakerDto>(row);
    }
}