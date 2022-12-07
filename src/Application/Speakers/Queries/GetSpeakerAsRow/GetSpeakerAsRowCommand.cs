using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Speakers.Commands.CreateSpeaker;

namespace SynopsisV2.Application.Speakers.Queries.GetSpeakerAsRow;

public record GetAsRowSpeakerCommand(int Id) : IRequest<SpeakerDto>;

public class GetSpeakerAsRowCommandHandler : IRequestHandler<GetAsRowSpeakerCommand, SpeakerDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSpeakerAsRowCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<SpeakerDto> Handle(GetAsRowSpeakerCommand request, CancellationToken cancellationToken)
    {
        {
            var row = await _dbContext.Speakers
                .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
                .ConfigureAwait(false);
            if (row is null)
            {
                throw new InvalidOperationException();
            }

            return _mapper.Map<SpeakerDto>(row); //Todo
        }
    }
}