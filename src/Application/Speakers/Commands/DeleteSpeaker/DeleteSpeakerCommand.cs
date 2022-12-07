using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;

namespace SynopsisV2.Application.Speakers.Commands.DeleteSpeaker;

public class DeleteSpeakerCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteSpeakerCommandHandler : IRequestHandler<DeleteSpeakerCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public DeleteSpeakerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteSpeakerCommand request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Speakers
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (row is null)
        {
            return Unit.Value;
        }

        _dbContext.Speakers.Remove(row);

        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
        return Unit.Value;
    }
}