using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Partners.Queries.GetPartner;

public record GetPartnerCommand(int Id) : IRequest<PartnerListDto>;

public class GetPartnerCommandHandler : IRequestHandler<GetPartnerCommand, PartnerListDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPartnerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<PartnerListDto> Handle(GetPartnerCommand request, CancellationToken cancellationToken)
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

        return _mapper.Map<PartnerListDto>(row);    }
}