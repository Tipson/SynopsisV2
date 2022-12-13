using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Speakers.Commands.CreateSpeaker;

namespace SynopsisV2.Application.Speakers.Commands.UpdateVisibilitySpeaker;

public record UpdateVisibilitySpeakerCommand(
    int Id,
    bool IsShow) : IRequest<SpeakerDto>;

public class UpdateVisibilitySpeakerCommandHandler : IRequestHandler<UpdateVisibilitySpeakerCommand, SpeakerDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateVisibilitySpeakerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<SpeakerDto> Handle(UpdateVisibilitySpeakerCommand request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Speakers
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);
        if (row is null)
        {
            throw new InvalidOperationException($"The Speaker with id = {request.Id} not found");
        }

        row.IsShow = request.IsShow ? 1:0;
        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
        return _mapper.Map<SpeakerDto>(row);    
    }
}