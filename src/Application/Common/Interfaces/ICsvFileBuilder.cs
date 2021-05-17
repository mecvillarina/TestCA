using System.Collections.Generic;
using TestCA.Application.TodoLists.Queries.ExportTodos;

namespace TestCA.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
