using System.Globalization;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.TodoLists.Queries.ExportTodos;
using SynopsisV2.Infrastructure.Files.Maps;
using CsvHelper;

namespace SynopsisV2.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
