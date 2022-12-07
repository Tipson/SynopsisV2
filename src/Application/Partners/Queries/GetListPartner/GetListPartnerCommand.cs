using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Domain.Enums;

namespace SynopsisV2.Application.Partners.Queries.GetListPartner;

public record GetListPartnerCommand(SynopsisVersionType VersionType) : IRequest<PartnerListDto>;

public class GetListPartnerCommandHandler : IRequestHandler<GetListPartnerCommand, PartnerListDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetListPartnerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<PartnerListDto> Handle(GetListPartnerCommand request, CancellationToken cancellationToken)
    {
        var rows = await _dbContext.Partners
            .Include(r => r.Speakers)
            .AsNoTracking()
            .Where(r => r.SynopsisType == (int)request.VersionType)
            .OrderByDescending(r=>r.Important)
            .ToListAsync(cancellationToken);

        var model = (_mapper.Map<List<Partner>>(rows));
        return _mapper.Map<Common.Models.PartnerListDto>(model); //Todo
    }
}