using AutoMapper;
using Duende.IdentityServer.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using Synopsis.Models.Speakers;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Speakers.Commands.CreateSpeaker;
using SynopsisV2.Domain.Enums;
using Speaker = SynopsisV2.Application.Common.Models.Speaker;

namespace SynopsisV2.Application.Speakers.Queries.GetSpeaker;
public record GetSpeakerCommand(int Id, 
    string Lang) : IRequest<Speaker>;

public class GetSpeakerCommandHandler : IRequestHandler<GetSpeakerCommand, Speaker>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSpeakerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<Speaker> Handle(GetSpeakerCommand request, CancellationToken cancellationToken)
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
        
        var model = _mapper.Map<Speaker>(row);
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