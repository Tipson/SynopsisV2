using System.Collections;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Partners.Queries.GetListPartner;

public record PartnerListDto(IEnumerable<PartnerDto> Partners);

