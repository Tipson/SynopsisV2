using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using Synopsis.Models.Partners;
using SynopsisV2.Domain.Enums;

namespace SynopsisV2.Application.Partners.Queries.GetGroupedPartner;

public record GetGroupedPartnerQuery(SynopsisVersionType SynopsisVersionType) : IRequest<PartnersGroupedCollection>;

public class GetGroupedPartnerQueryHandler : IRequestHandler<GetGroupedPartnerQuery, PartnersGroupedCollection>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGroupedPartnerQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<PartnersGroupedCollection> Handle(GetGroupedPartnerQuery request, CancellationToken cancellationToken)
    {
        var rows = await _dbContext.Partners
            .Include(r => r.Speakers)
            .Where(r => r.IsShow == 1 && r.SynopsisType == (int)request.SynopsisVersionType)
            .AsNoTracking()
            .OrderByDescending(r=>r.Important)
            .ToListAsync(cancellationToken);

        var models = _mapper.Map<List<Partner>>(rows);

        var partnersGroupByType = models.GroupBy(m => m.Type).ToList();

        return new PartnersGroupedCollection
        {
            MediaPartners = partnersGroupByType.SingleOrDefault(p => p.Key == (int?)PartnerType.MediaPartners)?.ToList(),
            Participating = partnersGroupByType.SingleOrDefault(p => p.Key == (int?)PartnerType.ParticipatingCompanies)?.ToList(),
            GoldPartners = partnersGroupByType.SingleOrDefault(p => p.Key == (int?)PartnerType.GoldPartners)?.ToList(),
            DiamondPartners = partnersGroupByType.SingleOrDefault(p => p.Key == (int?)PartnerType.DiamondPartners)?.ToList(),
            OfflinePartners = partnersGroupByType.SingleOrDefault(p => p.Key == (int?)PartnerType.OfflinePartners)?.ToList(),
            Sponsors = partnersGroupByType.SingleOrDefault(p => p.Key == (int?)PartnerType.Sponsors)?.ToList(),
            CoOrganizers = partnersGroupByType.SingleOrDefault(p => p.Key == (int?)PartnerType.CoOrganizers)?.ToList(),
            Organizers = partnersGroupByType.SingleOrDefault(p => p.Key == (int?)PartnerType.Organizers)?.ToList(),
            GeneralSponsors = partnersGroupByType.SingleOrDefault(p => p.Key == (int?)PartnerType.GeneralSponsor)?.ToList(),
            SilverSponsors = partnersGroupByType.SingleOrDefault(p => p.Key == (int?)PartnerType.SilverSponsors)?.ToList(),
            GoldSponsors = partnersGroupByType.SingleOrDefault(p => p.Key == (int?)PartnerType.GoldSponsors)?.ToList(),
            DiamondSponsors = partnersGroupByType.SingleOrDefault(p => p.Key == (int?)PartnerType.DiamondSponsors)?.ToList(),
        };    
    }
}