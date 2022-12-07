using SynopsisV2.Application.Common.Mappings;
using SynopsisV2.Domain.Entities;

namespace SynopsisV2.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
