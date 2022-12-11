using SynopsisV2.Application.Common.Models;
using SynopsisV2.Domain.Entities;

namespace SynopsisV2.Application.Agendas.Queries.GetGroupedAgendas;

public class AgendasGroupedCollection
{
    public IEnumerable<AgendaGroupedItem> AgendasGrouped { get; set; }
    
    public record AgendaGroupedItem(
        DateTimeOffset Day,
        List<Agenda> Agendas);
}