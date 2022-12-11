using System.Collections;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Agendas.Queries.GetListAgendas;

public record AgendaListDto(IEnumerable<AgendaDto> Agendas);

