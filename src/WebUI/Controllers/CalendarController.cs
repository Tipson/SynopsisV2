using Microsoft.AspNetCore.Mvc;
using SynopsisV2.Application.Agendas.Queries.GetAgenda;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class CalendarController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<string>> CreateAgendaGoogleCalendarLink([FromQuery]GetAgendaQuery query,
        CancellationToken token, string language)
    {
        var agenda = await Mediator.Send(query, token);
        foreach (var speaker in agenda.Speakers)
        {
            if (language == "En")
            {
                return
                    $"https://www.google.com/calendar/render?action=TEMPLATE" +
                    $"&text={speaker.NameEn}" +
                    $"&dates={agenda.StartTime:yyyyMMddTHmss}/{agenda.EndTime:yyyyMMddTHmss}" +
                    $"&details={agenda.TopicEn}&location=SynopsisEvents" +
                    $"&trp=false&sprop=&sprop=name:";
            }
            else if (language == "Ru")
            {
                return
                    $"https://www.google.com/calendar/render?action=TEMPLATE" +
                    $"&text={speaker.NameRu}" +
                    $"&dates={agenda.StartTime:yyyyMMddTHmss}/{agenda.EndTime:yyyyMMddTHmss}" +
                    $"&details={agenda.TopicRu}&location=SynopsisEvents" +
                    $"&trp=false&sprop=&sprop=name:";
            }
        }
        throw new InvalidOperationException();
    }
}