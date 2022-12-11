using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Errors.Queries.GetError;

public record GetErrorQuery(
    int Id) : IRequest<ErrorDto>;

public class GetErrorQueryHandler : IRequestHandler<GetErrorQuery, ErrorDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetErrorQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<ErrorDto> Handle(GetErrorQuery request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Errors
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (row is null)
        {
            throw new InvalidOperationException($"$The Error with id = {request.Id} not found");
        }

        return _mapper.Map<ErrorDto>(row);
    }
}