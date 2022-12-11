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
    public async Task<ActionResult<AgendaDto>> Get(GetAgendaCommand command)
    {
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<AgendaListDto>>> GetAll(GetListAgendasCommand command)
    {
        var result = await Mediator.Send(command)
            .ConfigureAwait(false);
        return Ok(result);
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult<AgendasGroupedCollection>> GetGrouped(GetGroupedAgendasCommand command)
    {
        var result = await Mediator.Send(command)
            .ConfigureAwait(false);
        return result;
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
    public async Task<ActionResult> Delete(DeleteAgendaCommand command)
    {
        await Mediator.Send(command)
            .ConfigureAwait(false);
        return NoContent();
    }
    
}