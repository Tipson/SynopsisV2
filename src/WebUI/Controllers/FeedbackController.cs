using Microsoft.AspNetCore.Mvc;
using SynopsisV2.Application.Feedbacks.Commands;

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