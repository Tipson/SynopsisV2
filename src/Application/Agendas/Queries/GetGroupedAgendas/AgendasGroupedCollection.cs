using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Agendas.Queries.GetGroupedAgendas;

public class AgendasGroupedCollection
{
    public IEnumerable<AgendaGroupedItem> AgendasGrouped { get; set; }
    
    public record AgendaGroupedItem(
        DateTimeOffset Day,
        IEnumerable<AgendaDto> Agendas);
}