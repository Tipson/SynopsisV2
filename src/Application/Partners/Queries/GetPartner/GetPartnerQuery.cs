using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Partners.Queries.GetPartner;

public record GetPartnerQuery(int Id) : IRequest<PartnerDto>;

public class GetPartnerQueryHandler : IRequestHandler<GetPartnerQuery, PartnerDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPartnerQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<PartnerDto> Handle(GetPartnerQuery request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Partners
            .Include(r => r.Speakers)
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (row is null)
        {
            throw new InvalidOperationException($"The Partners with id = {request.Id} not found");
        }

        return _mapper.Map<PartnerDto>(row);
    }
}