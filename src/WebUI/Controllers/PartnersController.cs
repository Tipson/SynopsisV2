using MediatR;
using Microsoft.AspNetCore.Mvc;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Agendas.Queries.GetGroupedAgendas;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Partners.Commands.CreatePartner;
using SynopsisV2.Application.Partners.Commands.DeletePartner;
using SynopsisV2.Application.Partners.Commands.UpdatePartner;
using SynopsisV2.Application.Partners.Commands.UpdateVisibilityPartner;
using SynopsisV2.Application.Partners.Queries.GetListPartner;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class PartnersController : ApiControllerBase
{
    [HttpGet("[action]")]
    public async Task<ActionResult<PartnerDto>> GetAll([FromQuery]GetListPartnerQuery command)
    {
        var result = await Mediator.Send(command)
            .ConfigureAwait(false);
        return Ok(result);
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult<PartnerDto>> GetGrouped([FromQuery]GetGroupedAgendasQuery query)
    {
        var result = await Mediator.Send(query)
            .ConfigureAwait(false);
        return Ok(result);
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult<PartnerDto>> Create([FromBody]CreatePartnerCommand command)
    {
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }

    [HttpPost("[action]")]
    public async Task<PartnerDto> Update([FromBody]UpdatePartnerCommand command)
    {
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }

    [HttpDelete("[action]")]
    public async Task<Unit> Delete([FromQuery]DeletePartnerCommand command)
    { 
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }

    [HttpPut("[action]")]
    public async Task<PartnerDto> UpdateVisibility([FromBody]UpdateVisibilityPartnerCommand command)
    {
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }
}