using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SynopsisV2.Application.Tickets.Queries.GetTicket;
using SynopsisV2.WebUI.Controllers;

namespace WebUI.Controllers;

[Route("[controller]")]
public class TicketsController : ApiControllerBase
{

    [Route("TicketCheck")]
    public async Task<ActionResult> TicketCheck([Required]string code, CancellationToken token = default)
    {
        HttpContext.Session.Set("Result", null);

        if (string.IsNullOrWhiteSpace(code))
        {
            return new RedirectResult("/" + HttpContext.Session.Get("Culture") + "/ticket?type=2");
        }

        var ticket = await Mediator.Send(new GetTicketQuery(code, token);

        if (ticket.Type != 0)
        {
            HttpContext.Session.Set("Result", BitConverter.GetBytes(1));
            HttpContext.Session.Set("ID", BitConverter.GetBytes(ticket.Id));
        }

        return new RedirectResult("/" + HttpContext.Session.Get("Culture") + "/ticket?type=2");
    }
    
    [Route("ticket_success")]
    public ActionResult TicketSuccess()
    {
        return new EmptyResult();
    }
    
    [HttpGet]
    public async Task<ActionResult> Ticket(int type, CancellationToken token = default)
    {
        if (HttpContext.Session.Get("Result") == null)
        {
            return new RedirectResult("/" + HttpContext.Session.Get("Culture"));
        }

        
        if (HttpContext.Session.Get("ID") == null)
        {
            return View();
        }
        
        var ticket = await _ticketService.Get(
            new GetTicketCommand(
                BitConverter.ToInt32(
                    HttpContext.Session.Get("ID"))),
            token)
            .ConfigureAwait(false);

        ViewBag.User = ticket;

        return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> Ticket(Ticket user, CancellationToken token)
    {

        if (HttpContext.Session.Get("ID") == null)
        {
            return View();
        }
        
        var ticket = await _ticketService.Update(
                new UpdateTicketCommand(
                    BitConverter.ToInt32(
                        HttpContext.Session.Get("ID")),
                    user.Telegram,
                    user.WalletBsc,
                    user.Name),
                token)
            .ConfigureAwait(false);
        ViewBag.User = user;

            return View();


    }

}