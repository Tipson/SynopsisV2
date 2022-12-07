using AutoMapper;
using Duende.IdentityServer.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Models;
using Synopsis.Models.Speakers;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Speakers.Commands.CreateSpeaker;
using SynopsisV2.Domain.Enums;

namespace SynopsisV2.Application.Speakers.Queries.GetListSpeakers;

public record GetListSpeakerCommand(SynopsisVersionType VersionType, 
    string Lang) : IRequest<SpeakerDto>;

public class GetListSpeakerCommandHandler : IRequestHandler<GetListSpeakerCommand, SpeakerDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetListSpeakerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<SpeakerDto> Handle(GetListSpeakerCommand request, CancellationToken cancellationToken)
    {
        var rows = await _dbContext.Speakers
            .Include(r => r.Partner)
            .Include(r => r.Sites)
            .Include(r=>r.Logos)
            .AsNoTracking()
            .Where(r => r.SynopsisType == (int)request.VersionType)
            .OrderByDescending(r => r.Important)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var models = _mapper.Map<List<SpeakerDto>>(rows);

        foreach (var model in models)
        {
            if (request.Lang == "ru")
            {
                model.Position = model.PositionRu;
                model.Name = model.NameRu;
            }
            else
            {
                model.Position = model.PositionEn;
                model.Name = model.NameEn;
            }

            model.TypeName = model.Type; //todo
        }
        return _mapper.Map<SpeakerDto>(models); //todo
    }
}