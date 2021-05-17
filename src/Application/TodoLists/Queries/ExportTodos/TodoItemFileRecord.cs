using TestCA.Application.Common.Mappings;
using TestCA.Domain.Entities;

namespace TestCA.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
