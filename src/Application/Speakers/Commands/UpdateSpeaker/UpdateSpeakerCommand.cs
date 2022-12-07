using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using Synopsis.Models;
using Synopsis.Models.Speakers;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Speakers.Commands.CreateSpeaker;
using SynopsisV2.Domain.Enums;
using SpeakerDto = SynopsisV2.Application.Common.Models.SpeakerDto;

namespace SynopsisV2.Application.Speakers.Commands.UpdateSpeaker;

public record UpdateSpeakerCommand(
    int Id,
    string NameRu,
    string NameEn,
    string PositionRu,
    string PositionEn,
    string Image,
    SpeakerType Type,
    SynopsisVersionType SynopsisType,
    ImportanceType Importance,
    bool IsShow,
    string? BiographyRu,
    string? BiographyEn,
    string? Twitter,
    string? Medium,
    string? Linkedin,
    List<UpdateSiteCommand> Sites,
    List<UpdateLogoCommand> Logos,
    bool IsFavorite = false,
    bool CommissionMember = false,
    int? PartnerId = null) : IRequest<SpeakerDto>;

public class UpdateSpeakerCommandHandler : IRequestHandler<UpdateSpeakerCommand, SpeakerDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateSpeakerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<SpeakerDto> Handle(UpdateSpeakerCommand request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Speakers
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (row is null)
        {
            throw new InvalidOperationException($"The Speaker with id = {request.Id} not found");
        }

        row.NameRu = request.NameRu;
        row.NameEn = request.NameEn;
        row.Photo = request.Image;
        row.IsCommission = request.CommissionMember ? 1 : 0;
        row.IsFavorite = request.IsFavorite ? 1 : 0;
        row.Important = (int)request.Importance;
        row.PositionEn = request.PositionEn;
        row.PositionRu = request.PositionRu;
        row.Type = (int)request.Type;
        row.SynopsisType = (int)request.SynopsisType;
        row.BiographyRu = request.BiographyRu;
        row.BiographyRu = request.BiographyEn;
        row.Twitter = request.Twitter;
        row.Medium = request.Medium;
        row.Linkedin = request.Linkedin;
        row.PartnerId = request.PartnerId;

        row.Sites = request.Sites.Select(e => new Site(row.Id, e.CompanyNameEn, e.CompanyNameRu, e.Url)).ToList();
        row.Logos = request.Logos.Select(e => new Logo(row.Id, e.Path)).ToList();

        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<SpeakerDto>(row);
    }
}