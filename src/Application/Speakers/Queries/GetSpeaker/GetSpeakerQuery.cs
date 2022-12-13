using AutoMapper;
using Duende.IdentityServer.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using Synopsis.Models.Speakers;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Speakers.Commands.CreateSpeaker;
using SynopsisV2.Domain.Enums;

namespace SynopsisV2.Application.Speakers.Queries.GetSpeaker;
public record GetSpeakerQuery(int Id, 
    string Lang) : IRequest<SpeakerDto>;

public class GetSpeakerQueryHandler : IRequestHandler<GetSpeakerQuery, SpeakerDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSpeakerQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<SpeakerDto> Handle(GetSpeakerQuery request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Speakers
            .Include(r => r.Partner)
            .Include(r=>r.Sites)
            .Include(r=>r.Logos)
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);
        
        if (row is null)
        {
            throw new InvalidOperationException($"The Speaker with id = {request.Id} not found");
        }
        
        var model = _mapper.Map<SpeakerDto>(row);
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
        return model;  
    }
}