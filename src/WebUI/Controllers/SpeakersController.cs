using MediatR;
using Microsoft.AspNetCore.Mvc;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Speakers.Commands.CreateSpeaker;
using SynopsisV2.Application.Speakers.Commands.UpdateSpeaker;
using SynopsisV2.Application.Speakers.Commands.UpdateVisibilitySpeaker;
using SynopsisV2.Application.Speakers.Queries.GetListSpeakers;
using SynopsisV2.Application.Speakers.Queries.GetSpeaker;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class SpeakersController : ApiControllerBase
{
    [HttpGet("[action]")]
    public async Task<ActionResult<SpeakerListDto>> GetAll([FromQuery]GetListSpeakerQuery command)
    {
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult<SpeakerDto>> Get([FromQuery]GetSpeakerQuery command)
    {
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }

    [HttpPost("[action]")]
    public async Task<SpeakerDto> Create([FromBody]CreateSpeakerCommand command)
    {
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult<SpeakerDto>> Update([FromBody]UpdateSpeakerCommand command)
    {
        return await Mediator.Send(command) 
            .ConfigureAwait(false);
    }

    [HttpPut("[action]")]
    public async Task<SpeakerDto> UpdateVisibility([FromQuery]UpdateVisibilitySpeakerCommand command)
    {
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }
}