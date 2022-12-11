using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Speakers.Commands.CreateSpeaker;

namespace SynopsisV2.Application.Speakers.Queries.GetSpeakerAsRow;

public record GetAsRowSpeakerQuery(int Id) : IRequest<Speaker>;

public class GetSpeakerAsRowQueryHandler : IRequestHandler<GetAsRowSpeakerQuery, Speaker>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSpeakerAsRowQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Speaker> Handle(GetAsRowSpeakerQuery request, CancellationToken cancellationToken)
    {
        {
            var row = await _dbContext.Speakers
                .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
                .ConfigureAwait(false);
            if (row is null)
            {
                throw new InvalidOperationException();
            }

            return _mapper.Map<Speaker>(row);
        }
    }
}