using System.Globalization;
using SynopsisV2.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace SynopsisV2.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
    }
}
