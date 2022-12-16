using Microsoft.AspNetCore.Mvc;
using SynopsisV2.Application.Feedbacks.Commands;
using SynopsisV2.WebUI.Controllers;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedbackController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Send(SendFeedbackCommand command)
    {
        await Mediator.Send(command);
        
        return NoContent();
    }
}