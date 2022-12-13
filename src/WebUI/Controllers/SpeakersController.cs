using MediatR;
using Microsoft.AspNetCore.Mvc;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Speakers.Commands.CreateSpeaker;
using SynopsisV2.Application.Speakers.Commands.UpdateSpeaker;
using SynopsisV2.Application.Speakers.Commands.UpdateVisibilitySpeaker;
using SynopsisV2.Application.Speakers.Queries.GetListSpeakers;
using SynopsisV2.WebUI.Controllers;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class SpeakersController : ApiControllerBase
{
    [HttpGet("[action]")]
    public async Task<ActionResult<SpeakerListDto>> GetAll(GetListSpeakerQuery command)
    {
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }

    [HttpPost("[action]")]
    public async Task<SpeakerDto> Create(CreateSpeakerCommand command)
    {
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult<SpeakerDto>> Update(UpdateSpeakerCommand command)
    {
        return await Mediator.Send(command) 
            .ConfigureAwait(false);
    }

    [HttpPut("[action]")]
    public async Task<SpeakerDto> UpdateVisibility(UpdateSpeakerCommand command)
    {
        return await Mediator.Send(command)
            .ConfigureAwait(false);
    }
}