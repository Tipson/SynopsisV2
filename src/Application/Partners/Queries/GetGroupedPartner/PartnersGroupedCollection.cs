using Synopsis.Infrastructure.DbContext.Entities;

namespace SynopsisV2.Application.Partners.Queries.GetGroupedPartner;

public class PartnersGroupedCollection
{
    public IEnumerable<Partner>? MediaPartners { get; init; }
    public IEnumerable<Partner>? Participating { get; init; }
    public IEnumerable<Partner>? GoldPartners { get; init; }
    public IEnumerable<Partner>? DiamondPartners { get; init; }
    public IEnumerable<Partner>? OfflinePartners { get; init; }
    public IEnumerable<Partner>? Sponsors { get; init; }
    public IEnumerable<Partner>? CoOrganizers { get; init; }
    public IEnumerable<Partner>? Organizers { get; init; }
    public IEnumerable<Partner>? GeneralSponsors { get; init; }
    public IEnumerable<Partner>? SilverSponsors { get; init; }
    public IEnumerable<Partner>? GoldSponsors { get; init; }
    public IEnumerable<Partner>? DiamondSponsors { get; init; }
}