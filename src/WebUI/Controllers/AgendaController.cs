using MediatR;
using Microsoft.AspNetCore.Mvc;
using SynopsisV2.Application.Agendas.Commands.CreateAgenda;
using SynopsisV2.Application.Agendas.Commands.DeleteAgenda;
using SynopsisV2.Application.Agendas.Commands.UpdateAgenda;
using SynopsisV2.Application.Agendas.Queries.GetAgenda;
using SynopsisV2.Application.Agendas.Queries.GetGroupedAgendas;
using SynopsisV2.Application.Agendas.Queries.GetListAgendas;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Domain.Entities;
using SynopsisV2.WebUI.Controllers;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class AgendaController : ApiControllerBase
{
    [HttpGet("[action]")]
    public async Task<ActionResult<AgendaDto>> Get(GetAgendaQuery query)
    {
        return await Mediator.Send(query)
            .ConfigureAwait(false);
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult<AgendaListDto>> GetAll(GetListAgendasQuery query)
    {
        return await Mediator.Send(query)
            .ConfigureAwait(false);
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult<AgendasGroupedCollection>> GetGrouped(GetGroupedAgendasQuery query)
    {
        return await Mediator.Send(query)
            .ConfigureAwait(false);
        
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<AgendaDto>> Create(CreateAgendaCommand command)
    {  
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }
    
    [HttpPost("[action]")]
    public async Task<AgendaDto> Update(UpdateAgendaCommand command)
    {
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }
    
    [HttpDelete("[action]")]
    public async Task<Unit> Delete(DeleteAgendaCommand command)
    {
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }
    
}